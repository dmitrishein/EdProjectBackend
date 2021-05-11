using AutoMapper;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EdProject.BLL.Services
{
    public class OrderService : IOrdersService
    {
        OrderRepository _orderRepository;
        PaymentRepository _paymentRepository;
        EditionRepository _editionRepository;
        IMapper _mapper;

        public OrderService(AppDbContext appDbContext, IMapper mapper)
        {
            _orderRepository = new OrderRepository(appDbContext);
            _paymentRepository = new PaymentRepository(appDbContext);
            _editionRepository = new EditionRepository(appDbContext);
            _mapper = mapper;
        }

        public async Task CreateOrderAsync(OrderModel orderModel)
        {
            var newOrder = _mapper.Map<OrderModel, Orders>(orderModel);

            await _orderRepository.CreateAsync(newOrder);
        }
        public async Task CreateItemInOrderAsync(OrderItemModel orderItemModel)
        {
            var item = await _editionRepository.FindByIdAsync(orderItemModel.EditionId);
            var order = await _orderRepository.FindByIdAsync(orderItemModel.OrderId);

            await _orderRepository.AddItemToOrderAsync(order, item);
        }
        public async Task CreatePaymentAsync(PaymentModel paymentModel)
        {
            var newPayment = _mapper.Map<PaymentModel, Payments>(paymentModel);
            await _paymentRepository.CreateAsync(newPayment);
            var order = await _orderRepository.FindByIdAsync(paymentModel.OrderId);
            await _orderRepository.AddPaymentToOrderAsync(order, newPayment);
        }


        public async Task<List<OrderModel>> GetOrdersByUserIdAsync(long userId)
        {
            List<Orders> queryList = (await _orderRepository.GetAllOrdersAsync()).Where(x => x.Id == userId).ToList();

            if (!queryList.Any())
                throw new CustomException(Constant.NOTHING_FOUND, HttpStatusCode.OK);

            return _mapper.Map<List<Orders>, List<OrderModel>>(queryList);

        }
        public async Task<List<OrderModel>> GetOrdersListAsync()
        {
            if (!(await _orderRepository.GetAllOrdersAsync()).Any())
                throw new CustomException(Constant.NOTHING_FOUND, HttpStatusCode.OK);

            var ordersList = await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<List<Orders>, List<OrderModel>>(ordersList);
        }
        public async Task<OrderModel> GetOrderByIdAsync(long orderId)
        {
            var query = (await _orderRepository.GetAllOrdersAsync()).Where(o => o.Id == orderId);
            if (!query.Any())
                throw new CustomException(Constant.NOTHING_FOUND, HttpStatusCode.BadRequest);

            return _mapper.Map<Orders, OrderModel>(query.FirstOrDefault());
        }
        public async Task<List<EditionModel>> GetItemsInOrderAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);

            var orderItemList = order.Editions.Where(i => !i.IsRemoved).ToList();

            return _mapper.Map<List<Edition>, List<EditionModel>>(orderItemList);
        }
        public async Task<PaymentModel> GetPaymentInOrderAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);

            var payment = order.Payment;

            return _mapper.Map<Payments, PaymentModel>(payment);
        }

        public async Task RemoveItemFromOrderAsync(OrderItemModel orderItemModel)
        {
            var item = await _editionRepository.FindByIdAsync(orderItemModel.EditionId);
            var order = await _orderRepository.FindByIdAsync(orderItemModel.OrderId);

            await _orderRepository.RemoveItemToOrderAsync(order, item);
        }

    }
}
