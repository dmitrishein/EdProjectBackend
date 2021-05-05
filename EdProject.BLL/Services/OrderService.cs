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
        IMapper _mapper;

        public OrderService(AppDbContext appDbContext,IMapper mapper)
        {
            _orderRepository = new OrderRepository(appDbContext);
            _orderItemRepository = new OrderItemRepository(appDbContext);
            _paymentRepository = new PaymentRepository(appDbContext);
            _mapper = mapper;
        }

        public async Task CreateOrderAsync(OrderModel orderModel)
        {
            var newOrder = _mapper.Map<OrderModel, Orders>(orderModel);
            await _orderRepository.CreateAsync(newOrder);
        }
        public async Task CreateOrderItemAsync(OrderItemModel orderItemModel)
        {
            var newOrderItem = _mapper.Map<OrderItemModel, OrderItems>(orderItemModel);
            await _orderItemRepository.CreateAsync(newOrderItem);
        }
        public async Task CreatePaymentAsync(PaymentModel paymentModel)
        {  
            var newPayment = _mapper.Map<PaymentModel, Payments>(paymentModel);
            await _paymentRepository.CreateAsync(newPayment);
        }
        public List<OrderModel> GetOrdersByUserId(long userId)
        {
            try
            {     
                List<Orders> queryList = _orderRepository.GetAllOrders().Where(x => x.Id == userId).ToList();        

                if (!queryList.Any())
                    throw new Exception("No orders found");

                return _mapper.Map<List<Orders>, List<OrderModel>>(queryList);
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

                if (!_orderRepository.GetAllOrders().Any())
                    throw new Exception("No orders found");

                return _mapper.Map<List<Orders>,List<OrderModel>>(_orderRepository.GetAllOrders());
         
            }
            catch (Exception x)
            {
                throw new Exception($"Error!. {x.Message}");
            }
        }

    }
}
