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
            if (_orderItemRepository.IsExist(newOrderItem))
                throw new CustomException("Cannot create item, because it already exist");

            await _orderItemRepository.CreateAsync(newOrderItem);
        }
        public async Task CreatePaymentAsync(PaymentModel paymentModel)
        {  
            var newPayment = _mapper.Map<PaymentModel, Payments>(paymentModel);

            await _paymentRepository.CreateAsync(newPayment);
        }
        public async Task<List<OrderModel>> GetOrdersByUserId(long userId)
        {
            List<Orders> queryList = (await _orderRepository.GetAllOrders()).Where(x => x.Id == userId).ToList();

            if (!queryList.Any())
                    throw new CustomException("No orders found",200);

            return _mapper.Map<List<Orders>, List<OrderModel>>(queryList);
        }
        public async Task<List<OrderModel>> GetOrdersList()
        {
            if (!(await _orderRepository.GetAllOrders()).Any())
                throw new CustomException("No orders found :(",200);

            return _mapper.Map<List<Orders>, List<OrderModel>>(await _orderRepository.GetAllOrders());  
        }
        public async Task <OrderModel> GetOrderById(long orderId)
        {
            var query = (await _orderRepository.GetAllOrders()).Where(o => o.Id == orderId);
            if (!query.Any())
                throw new CustomException("No orders found :(", 200);
            
            return _mapper.Map<Orders, OrderModel>(query.FirstOrDefault());
        }

    }
}
