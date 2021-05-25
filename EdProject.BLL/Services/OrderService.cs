using AutoMapper;
using EdProject.BLL.Common.Options;
using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
            var usersOrderList = await _orderRepository.GetOrderByUserIdAsync(orderModel.UserId);
            //remove empty orders
            if(usersOrderList.Any(order => !order.Editions.Any()))
            {
                foreach(var item in usersOrderList)
                {
                    if(!item.Editions.Any())
                    {
                        await _orderRepository.DeleteAsync(item);
                    }
                }
            }

            var newOrder = _mapper.Map<OrderModel, Orders>(orderModel);
            newOrder.StatusType = DAL.Enums.PaidStatusType.Unpaid;
            await _orderRepository.CreateAsync(newOrder);
        }
        public async Task CreateItemInOrderAsync(OrderItemModel orderItemModel)
        {
            var item = await _editionRepository.FindByIdAsync(orderItemModel.EditionId);
            var order = await _orderRepository.FindByIdAsync(orderItemModel.OrderId);

            if(order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER,HttpStatusCode.BadRequest);
            }
            if(item is null || item.IsRemoved)
            {
                throw new CustomException(ErrorConstant.ITEM_NOT_FOUND, HttpStatusCode.BadRequest);
            }
            if(order.Editions.Any(edit=> edit.Id == orderItemModel.EditionId))
            {
                throw new CustomException($"{ErrorConstant.CANNOT_ADD_EDITION}.{ErrorConstant.ALREADY_EXIST}", HttpStatusCode.BadRequest);
            }
            await _orderRepository.AddItemToOrderAsync(order, item);
        }
        public async Task CreateItemsListInOrderAsync(OrderItemsListModel orderItemlistModel)
        {
            var order = await _orderRepository.FindByIdAsync(orderItemlistModel.OrderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            string[] editionsId = orderItemlistModel.Editions.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

            List<Edition> editionToAddList = new List<Edition>();

            foreach (var edition in editionsId)
            {
                var tempEdition = await _editionRepository.FindByIdAsync(int.Parse(edition));

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    continue;
                }

                editionToAddList.Add(tempEdition);
            }

            await _orderRepository.AddItemListToOrderAsync(order, editionToAddList);
        }
        public async Task CreatePaymentAsync(PaymentModel paymentModel)
        {
            var order = await _orderRepository.FindByIdAsync(paymentModel.OrderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }
            if (!order.Editions.Any())
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }
            if (order.StatusType is DAL.Enums.PaidStatusType.Paid)
            {
                throw new CustomException(ErrorConstant.PAYMENT_ALREADY_PAID, HttpStatusCode.BadRequest);
            }

            StripeConfiguration.ApiKey = _conectStripeOption.SecretKey;
            var options = new ChargeCreateOptions
            {
                Amount = paymentModel.Amount,
                Currency = paymentModel.Currency.ToString().ToLower(),
                Description = paymentModel.OrderId.ToString(),
                Source = paymentModel.PaymentSource
            };
            var service = new ChargeService();
            var charge = service.Create(options);
            paymentModel.TransactionId = charge.Id;

            if (charge.Status is not "succeeded")
            {
                throw new CustomException(ErrorConstant.UNSUCCESSFUL_PAYMENT, HttpStatusCode.OK);
            }

            var newPayment = _mapper.Map<PaymentModel, Payments>(paymentModel);
            order.StatusType = DAL.Enums.PaidStatusType.Paid;
            await _paymentRepository.CreateAsync(newPayment);
            await _orderRepository.AddPaymentToOrderAsync(order, newPayment);
        }


        public async Task<List<OrderModel>> GetOrdersByUserIdAsync(long userId)
        {
            List<Orders> queryList = (await _orderRepository.GetAllOrdersAsync()).Where(x => x.Id == userId).ToList();
            if (!queryList.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest);
            }
            return _mapper.Map<List<Orders>, List<OrderModel>>(queryList);
        }
        public async Task<List<OrderModel>> GetOrdersListAsync()
        {
            var ordersList = await _orderRepository.GetAllOrdersAsync();
            if (!ordersList.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.OK);
            }
            return _mapper.Map<List<Orders>, List<OrderModel>>(ordersList);
        }
        public async Task<List<OrderModel>> GetOrdersPageAsync(FilterPageModel pageModel)
        {
            var query = await _orderRepository.OrdersPage(pageModel.PageNumber, pageModel.ElementsAmount, pageModel.SearchString);
            if (!query.Any())
            {
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.OK);
            }
            return _mapper.Map<List<Orders>, List<OrderModel>>(query);
        }
        public async Task<OrderModel> GetOrderByIdAsync(long orderId)
        {
            var queryItem = await _orderRepository.FindByIdAsync(orderId);
               
            if (queryItem is null || queryItem.IsRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }
            return _mapper.Map<Orders, OrderModel>(queryItem);
                
        }
        public async Task<List<EditionModel>> GetItemsInOrderAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }
            var orderItemList = order.Editions.Where(i => !i.IsRemoved).ToList();

            return _mapper.Map<List<Edition>, List<EditionModel>>(orderItemList);
        }
        public async Task<PaymentModel> GetPaymentInOrderAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            var payment = order.Payment;
            if(payment is null || payment.IsRemoved)
            {
                throw new CustomException(ErrorConstant.ITEM_NOT_FOUND, HttpStatusCode.OK);
            }  

            return _mapper.Map<Payments, PaymentModel>(payment);
        }


        public async Task RemoveItemFromOrderAsync(OrderItemModel orderItemModel)
        {
            var item = await _editionRepository.FindByIdAsync(orderItemModel.EditionId);
            if(item is null)
            {
                throw new CustomException(ErrorConstant.ITEM_NOT_FOUND, HttpStatusCode.BadRequest); 
            }

            var order = await _orderRepository.FindByIdAsync(orderItemModel.OrderId);
            if(order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }    

            await _orderRepository.RemoveItemToOrderAsync(order, item);
        }
        public async Task RemoveItemsListFromOrder(OrderItemsListModel orderItemsListModel)
        {
            var order = await _orderRepository.FindByIdAsync(orderItemsListModel.OrderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            string[] editionsId = orderItemsListModel.Editions.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
            List<Edition> itemsToRemoveList = new List<Edition>();

            foreach (var edition in editionsId)
            {
                var tempEdition = await _editionRepository.FindByIdAsync(int.Parse(edition));

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    continue;
                }

                itemsToRemoveList.Add(tempEdition);
            }

            await _orderRepository.RemoveItemListFromOrderAsync(order, itemsToRemoveList);
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
