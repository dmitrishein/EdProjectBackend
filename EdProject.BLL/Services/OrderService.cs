using AutoMapper;
using EdProject.BLL.Common.Options;
using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Entities.Enums;
using EdProject.DAL.Enums;
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
        public async Task CreateItemsInOrderAsync(List<OrderItemModel> orderItemlistModel)
        {
            var order = await _orderRepository.FindByIdAsync(orderItemlistModel.First().OrderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            var editionList = await _editionRepository.GetAllEditionsAsync();

            foreach (var item in orderItemlistModel)
            {
                var tempEdition = editionList.Find(edition => edition.Id == item.EditionId);

                if (tempEdition is null)
                {
                    continue;
                }
                if (order.OrderItems.Any(orderItem => orderItem.EditionId == item.EditionId))
                {
                    continue;
                }

                item.Currency = tempEdition.Currency;
                item.Amount = tempEdition.Price * item.ItemsCount;

                var orderItem = _mapper.Map<OrderItem>(item);
                order.OrderItems.Add(orderItem);
            }

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

            long orderTotalCost = Convert.ToInt64(_orderRepository.GetOrderCost(order));
            StripeConfiguration.ApiKey = _conectStripeOption.SecretKey;
            var options = new ChargeCreateOptions
            {
                Amount = orderTotalCost,
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
            var orderList = await _orderRepository.GetOrderByUserIdAsync(userId);
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
        public async Task<List<OrderModel>> GetOrdersPageAsync(FilterPageModel pageModel)
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


        public async Task UpdateOrderItemAsync(UpdateOrderItem orderItem)
        {
            var order = await _orderRepository.FindByIdAsync(orderItem.OrderId);
            if(order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER,HttpStatusCode.BadRequest);
            }

            var updItem = _mapper.Map<OrderItem>(orderItem);
         
            await _orderRepository.UpdateOrderItem(order,updItem);
        }

        public async Task ClearOrder(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            if(order is null)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }
            await _orderRepository.ClearOrderByIdAsync(order);
        }

        public async Task RemoveItemsFromOrder(List<OrderItemModel> orderItemsListModel)
        {
            var order = await _orderRepository.FindByIdAsync(orderItemsListModel.First().OrderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            var orderItems = order.OrderItems;
            foreach (var item in orderItemsListModel)
            {
                var itemToRemove = orderItems.Find(i => i.EditionId == item.EditionId);

                if (itemToRemove is null)
                {
                    continue;
                }

                order.OrderItems.Remove(itemToRemove);
            }
            await _orderRepository.UpdateAsync(order);
        }
        public async Task RemoveOrderByIdAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            if(order.Editions.Any())
            {
                throw new CustomException(ErrorConstant.REMOVE_ORDER_ERROR,HttpStatusCode.BadRequest);
            }
            order.IsRemoved = true;

            await _orderRepository.UpdateAsync(order);
        }
       
    }
}
