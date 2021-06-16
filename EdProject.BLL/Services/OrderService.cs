using AutoMapper;
using EdProject.BLL.Common.Options;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Entities.Enums;
using EdProject.DAL.Enums;
using EdProject.DAL.Models;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using OrderItem = EdProject.DAL.Entities.OrderItem;

namespace EdProject.BLL.Services
{
    public class OrderService : IOrdersService
    {
        IOrderRepository _orderRepository;
        IPaymentRepository _paymentRepository;
        IEditionRepository _editionRepository;
        IMapper _mapper;
        private readonly StripeOptions _conectStripeOption;

        public OrderService(IMapper mapper, IOptions<StripeOptions> options,IOrderRepository orderRepository,
                            IPaymentRepository paymentRepository,IEditionRepository editionRepository)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _editionRepository = editionRepository;
            _mapper = mapper;
            _conectStripeOption = options.Value;
        }

        public async Task CreateOrderAsync(OrderModel orderModel)
        {
            var newOrder = _mapper.Map<Orders>(orderModel);

            await _orderRepository.CreateAsync(newOrder);
        }
        public async Task CreateItemsInOrderAsync(List<OrderItemModel> orderItemsModel)
        {
            var order = await _orderRepository.FindByIdAsync(orderItemsModel.First().OrderId);
            if (order is null || order.IsRemoved || order.StatusType.Equals(PaidStatusType.Paid))
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            var itemsId = orderItemsModel.Select(item => item.EditionId).ToList();
            var editionList = (await _editionRepository.GetEditionRangeAsync(itemsId)).Select(item => item.Id);
            if(!editionList.Any())
            {
                throw new CustomException(ErrorConstant.ITEM_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            orderItemsModel.RemoveAll(edit => !editionList.Contains(edit.EditionId));
            var orderItems = _mapper.Map<List<OrderItem>>(orderItemsModel);

            order.OrderItems.AddRange(orderItems);
            await _orderRepository.SaveChangesAsync();
        }
        public async Task CreatePaymentAsync(PaymentModel paymentModel)
        {
            var order = await _orderRepository.FindByIdAsync(paymentModel.OrderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }
            if (!order.OrderItems.Any())
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }
            if (order.StatusType is PaidStatusType.Paid)
            {
                throw new CustomException(ErrorConstant.PAYMENT_ALREADY_PAID, HttpStatusCode.BadRequest);
            }

            var orderPrice = _orderRepository.GetOrderCost(order);

            StripeConfiguration.ApiKey = _conectStripeOption.SecretKey;
            var options = new ChargeCreateOptions
            {
                Amount = (long)(orderPrice * VariableConstant.CONVERT_TO_CENT_VALUE),
                Currency = CurrencyTypes.USD.ToString().ToLower(),
                Description = $"#{order.Id}",
                Source = paymentModel.PaymentSource   
            };

            var service = new ChargeService();
            var charge = service.Create(options);

            if (!charge.Status.Equals(VariableConstant.CHARGE_SUCCEEDED))
            {
                order.StatusType = PaidStatusType.Unpaid;
                throw new CustomException(ErrorConstant.UNSUCCESSFUL_PAYMENT, HttpStatusCode.OK);
            }

            var newPayment = _mapper.Map<Payments>(paymentModel);
            order.StatusType = PaidStatusType.Paid;
            order.Payment = newPayment;
            newPayment.TransactionId = charge.Id;

            await _paymentRepository.CreateAsync(newPayment);
        }


        public async Task<List<OrderModel>> GetOrdersByUserIdAsync(long userId)
        {
            var orderList = await _orderRepository.GetOrdersByUserIdAsync(userId);
            if (!orderList.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest);
            }
            return _mapper.Map<List<OrderModel>>(orderList);
        }
        public async Task<List<OrderModel>> GetOrdersListAsync()
        {
            var ordersList = await _orderRepository.GetAllOrdersAsync();
            if (!ordersList.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.OK);
            }

            return _mapper.Map<List<OrderModel>>(ordersList);
        }
        public async Task<List<OrderModel>> GetOrdersPageAsync(EditionPageParameters pageModel)
        {
            var resultPage = await _orderRepository.OrdersPage(pageModel.PageNumber, pageModel.ElementsAmount, pageModel.SearchString);
            if (!resultPage.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.OK);
            }

            return _mapper.Map<List<OrderModel>>(resultPage);
        }
        public async Task<OrderModel> GetOrderByIdAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }

            return _mapper.Map<OrderModel>(order);          
        }
        public async Task<List<OrderItemModel>> GetItemsInOrderAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            var orderItemList = order.OrderItems.ToList();

            return _mapper.Map<List<OrderItemModel>>(orderItemList);
        }


        public async Task UpdateOrderItemAsync(OrderItemModel orderItem)
        {
            var order = await _orderRepository.FindByIdAsync(orderItem.OrderId);
            if (order is null || order.IsRemoved || order.StatusType.Equals(PaidStatusType.Paid))
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER,HttpStatusCode.BadRequest);
            }

            var item = order.OrderItems.First(ed => ed.EditionId == orderItem.EditionId);
            if(item is null)
            {
                throw new CustomException(ErrorConstant.INCORRECT_EDITION,HttpStatusCode.BadRequest);
            }

            item.ItemsCount = orderItem.ItemsCount;
            if(item.ItemsCount is VariableConstant.EMPTY)
            {
                order.OrderItems.Remove(item);
            }    

            await _orderRepository.UpdateAsync(order);
        }
        public async Task ClearOrder(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            if(order is null)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }
            order.OrderItems.Clear();
            await _orderRepository.UpdateAsync(order);
        }

        public async Task RemoveOrderByIdAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            order.IsRemoved = true;
            await _orderRepository.UpdateAsync(order);
        }
       
    }
}
