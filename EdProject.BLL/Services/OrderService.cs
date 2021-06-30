﻿using AutoMapper;
using EdProject.BLL.Common.Options;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Entities.Enums;
using EdProject.DAL.Enums;
using EdProject.DAL.Models;
using EdProject.DAL.Pagination.Models;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<long> CreateOrderAsync(string token, OrderCreateModel orderCreateModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer ", string.Empty);
            var userToken = tokenHandler.ReadJwtToken(token);
            var userId = userToken.Claims.First(claim => claim.Type == "id").Value;

            var orderItems = _mapper.Map<List<OrderItem>>(orderCreateModel.OrderItems);

            Orders userOrder = new Orders
            {
                OrderItems = orderItems,
                UserId = Convert.ToInt64(userId)
            };
            await _orderRepository.CreateAsync(userOrder);

            var Editions = await _editionRepository.GetEditionRangeAsync(orderCreateModel.OrderItems.Select(x => x.EditionId).ToList());
            userOrder.Editions = Editions;
            var orderPrice = userOrder.Editions.Sum(x => x.Price * userOrder.OrderItems.Find(y => y.EditionId == x.Id).ItemsCount);

            StripeConfiguration.ApiKey = _conectStripeOption.SecretKey;
            var options = new ChargeCreateOptions
            {
                Amount = (long)(orderPrice * VariableConstant.CONVERT_TO_CENT_VALUE),
                Currency = CurrencyTypes.USD.ToString().ToLower(),
                Description = $"Order #{userOrder.Id}",
                Source = orderCreateModel.SourceId
            };

            var service = new ChargeService();
            var charge = service.Create(options);

            //unsuccess payment
            if (!charge.Status.Equals(VariableConstant.CHARGE_SUCCEEDED))
            {
                userOrder.StatusType = PaidStatusType.Unpaid;
                throw new CustomException(ErrorConstant.UNSUCCESSFUL_PAYMENT, HttpStatusCode.OK);
            }

            //success payment
            Payments newPayment = new Payments
            {
                TransactionId = charge.Id,
                Amount = orderPrice,
                Currency = CurrencyTypes.USD
            };
            await _paymentRepository.CreateAsync(newPayment);


            userOrder.StatusType = PaidStatusType.Paid;
            userOrder.Payment = newPayment;
            userOrder.Description = $"OrderId:{userOrder.Id}| PaymentId : {newPayment.Id}";
            userOrder.Total = orderPrice;

            await _orderRepository.SaveChangesAsync();
            return userOrder.Id;
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
        public async Task<OrdersPageResponseModel> GetOrdersPageAsync(string token,OrdersPageParameters pageParams)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer ", string.Empty);
            var userToken = tokenHandler.ReadJwtToken(token);
            var userId = userToken.Claims.First(claim => claim.Type == "id").Value;
            pageParams.UserId = userId;
            var resultPage = await _orderRepository.OrdersPage(pageParams);
            var lis = _mapper.Map<OrdersPageResponseModel>(resultPage);
            return lis;
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
            var order = await _orderRepository.FindByIdAsync(1);
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
