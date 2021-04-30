using AutoMapper;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class OrderService : IOrdersService
    {
        OrderRepository _orderRepository;
        OrderItemRepository _orderItemRepository;
        PaymentRepository _paymentRepository;

        public OrderService(AppDbContext appDbContext)
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
        public List<OrderModel> GetOrdersByUserId(long userId)
        {
            try
            {
                List<OrderModel> outList = new List<OrderModel>();
                List<Orders> queryList = _orderRepository.GetAll().Where(x => x.Id == userId).ToList();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Orders, OrderModel>());
                var _mapper = new Mapper(config);

                if (queryList.Count() == 0)
                    throw new Exception("No orders found");

                foreach (Orders orders in queryList)
                {
                    var orderOut = _mapper.Map<Orders, OrderModel>(orders);
                    outList.Add(orderOut);
                }

                return outList;
            }
            catch (Exception x)
            {
                throw new Exception($"Error!. {x.Message}");
            }

        }
        public List<OrderModel> GetOrdersList()
        {
            try
            {
                List<OrderModel> outList = new List<OrderModel>();
                List<Orders> queryList = _orderRepository.GetAll().ToList();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Orders, OrderModel>());
                var _mapper = new Mapper(config);

                if (queryList.Count() == 0)
                    throw new Exception("No orders found");

                foreach (Orders orders in queryList)
                {
                    var orderOut = _mapper.Map<Orders, OrderModel>(orders);
                    outList.Add(orderOut);
                }

                return outList;
            }
            catch (Exception x)
            {
                throw new Exception($"Error!. {x.Message}");
            }
        }

    }
}
