using AutoMapper;
using EdProject.BLL.Common.Options;
using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.User;
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
            var order = await _orderRepository.FindByIdAsync(orderItemModel.OrderId);

            var editionToOrder = await _editionRepository.FindByIdAsync(orderItemModel.EditionId);

            orderItemModel.Currency = editionToOrder.Currency;
            orderItemModel.Amount = editionToOrder.Price * orderItemModel.ItemsCount;

            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER,HttpStatusCode.BadRequest);
            }
          
            var item = _mapper.Map<OrderItemModel, DAL.Entities.OrderItem>(orderItemModel);
   
            if(order.OrderItems.Any(orderItem => orderItem.EditionId == item.EditionId))
            {
                throw new CustomException(ErrorConstant.ALREADY_EXIST, HttpStatusCode.BadRequest);
            }
            await _orderRepository.AddItemToOrderAsync(order, item);
        }
        public async Task CreateItemsListInOrderAsync(List<OrderItemModel> orderItemlistModel)
        {
            var order = await _orderRepository.FindByIdAsync(orderItemlistModel.First().OrderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            foreach (var item in orderItemlistModel)
            {
                var tempEdition = await _editionRepository.FindByIdAsync(item.EditionId);

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    orderItemlistModel.Remove(item);
                    continue;
                }
                if (order.OrderItems.Any(orderItem => orderItem.EditionId == item.EditionId))
                {
                    orderItemlistModel.Remove(item);
                    continue;
                }

                item.Currency = tempEdition.Currency;
                item.Amount = tempEdition.Price * item.ItemsCount;
            }

            var listToAdd = _mapper.Map<List<OrderItemModel>, List<DAL.Entities.OrderItem>>(orderItemlistModel);
            await _orderRepository.AddItemListToOrderAsync(order, listToAdd);
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
        public async Task<List<OrderItemModel>> GetItemsInOrderAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }
            var orderItemList = order.OrderItems.ToList();

            return _mapper.Map<List<DAL.Entities.OrderItem>, List<OrderItemModel>>(orderItemList);
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


        public async Task UpdateOrderItemAsync(UpdateOrderItem orderItem)
        {
            var order = await _orderRepository.FindByIdAsync(orderItem.OrderId);
            if(order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER,HttpStatusCode.BadRequest);
            }
            var updItem = _mapper.Map<UpdateOrderItem, DAL.Entities.OrderItem>(orderItem);
         
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
        public async Task RemoveItemFromOrderAsync(OrderItemModel orderItemModel)
        {
            var item = _mapper.Map<OrderItemModel, DAL.Entities.OrderItem>(orderItemModel);
            if (item is null)
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
        public async Task RemoveItemsListFromOrder(List<OrderItemModel> orderItemsListModel)
        {
            var order = await _orderRepository.FindByIdAsync(orderItemsListModel.First().OrderId);
            if (order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            foreach (var item in orderItemsListModel)
            {
                var tempEdition = await _editionRepository.FindByIdAsync(item.EditionId);

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    orderItemsListModel.Remove(item);
                    continue;
                }

                item.Currency = tempEdition.Currency;
                item.Amount = tempEdition.Price * item.ItemsCount;
            }

            var listToRemove = _mapper.Map<List<OrderItemModel>, List<DAL.Entities.OrderItem>>(orderItemsListModel);
            
            await _orderRepository.RemoveItemListFromOrderAsync(order, listToRemove);
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
