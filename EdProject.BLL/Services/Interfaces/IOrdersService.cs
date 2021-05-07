﻿using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IOrdersService 
    {
        public Task CreateOrderAsync(OrderModel orderModel);
        public Task CreateOrderItemAsync(OrderItemModel orderModel);
        public Task CreatePaymentAsync(PaymentModel paymentModel);
        public Task<List<OrderModel>> GetOrdersByUserId(long userId);
        public Task<List<OrderModel>> GetOrdersList();
        public Task<OrderModel> GetOrderById(long orderId);
    }
}
