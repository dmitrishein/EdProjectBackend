using AutoMapper;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class OrdersService : IOrdersService
    {
        OrderRepository _orderRepository;
        OrderItemRepository _orderItemRepository;
        PaymentRepository _paymentRepository;

        public OrdersService(AppDbContext appDbContext)
        {
            _orderRepository = new OrderRepository(appDbContext);
            _orderItemRepository = new OrderItemRepository(appDbContext);
            _paymentRepository = new PaymentRepository(appDbContext);
        }

        public async Task CreateOrderAsync(OrderModel orderModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderModel, Orders>());
            var _mapper = new Mapper(config);
            var newOrder = _mapper.Map<OrderModel, Orders>(orderModel);
           
            await _orderRepository.CreateAsync(newOrder);
        }

        public async Task CreateOrderItemAsync(OrderItemModel orderItemModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemModel, OrderItems>());
            var _mapper = new Mapper(config);
            var newOrderItem = _mapper.Map<OrderItemModel, OrderItems>(orderItemModel);

            await _orderItemRepository.CreateAsync(newOrderItem);
        }

        public async Task CreatePaymentAsync(PaymentModel paymentModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PaymentModel, Payments>());
            var _mapper = new Mapper(config);
            var newPayment = _mapper.Map<PaymentModel, Payments>(paymentModel);

            await _paymentRepository.CreateAsync(newPayment);
        }

        public async Task<IEnumerable<Orders>> GetOrdersListByUserId(long userId)
        {
            return await _orderRepository.GetOrderByUserId(userId);
        }

        public async Task<IEnumerable<Orders>> GetOrdersList()
        {
            return await _orderRepository.GetAsync();
        }
    }
}
