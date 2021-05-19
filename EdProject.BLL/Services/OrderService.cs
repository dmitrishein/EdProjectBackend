using AutoMapper;
using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
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
            if(_orderRepository.OrderExist(newOrder))
            {
                throw new CustomException(ErrorConstant.ALREADY_EXIST, HttpStatusCode.BadRequest);
            }

            await _orderRepository.CreateAsync(newOrder);
        }
        public async Task CreateItemInOrderAsync(OrderItemModel orderItemModel)
        {
            var item = await _editionRepository.FindByIdAsync(orderItemModel.EditionId);
            var order = await _orderRepository.FindByIdAsync(orderItemModel.OrderId);

            await _orderRepository.AddItemToOrderAsync(order, item);
        }
        public async Task CreateItemsListInOrderAsync(OrderItemsListModel orderItemlistModel)
        {
            var order = await _orderRepository.FindByIdAsync(orderItemlistModel.OrderId);
            OrderCheck(order);
            string[] editionsId = orderItemlistModel.Editions.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
            List<Edition> editionList = new List<Edition>();

            foreach (var edition in editionsId)
            {
                var tempEdition = await _editionRepository.FindByIdAsync(int.Parse(edition));

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    throw new CustomException($"Incorrect edition Id{edition}", HttpStatusCode.BadRequest);
                }

                editionList.Add(tempEdition);
            }

            await _orderRepository.AddItemListToOrderAsync(order, editionList);
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
            OrderListCheck(queryList);
            return _mapper.Map<List<Orders>, List<OrderModel>>(queryList);
        }
        public async Task<List<OrderModel>> GetOrdersListAsync()
        {
            var ordersList = await _orderRepository.GetAllOrdersAsync();
            OrderListCheck(ordersList);
            return _mapper.Map<List<Orders>, List<OrderModel>>(ordersList);
        }
        public async Task<List<OrderModel>> GetOrdersPageAsync(PageModel pageModel)
        {
            PageModelValidation(pageModel);
            var query = await _orderRepository.OrdersPage(pageModel.PageNumber, pageModel.ElementsAmount, pageModel.SearchString);
            OrderListCheck(query);
            return _mapper.Map<List<Orders>, List<OrderModel>>(query);
        }
        public async Task<OrderModel> GetOrderByIdAsync(long orderId)
        {
            var queryItem = (await _orderRepository.GetAllOrdersAsync()).Where(o => o.Id == orderId).FirstOrDefault();
            OrderCheck(queryItem);
            return _mapper.Map<Orders, OrderModel>(queryItem);
        }
        public async Task<List<EditionModel>> GetItemsInOrderAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            OrderCheck(order);
            var orderItemList = order.Editions.Where(i => !i.IsRemoved).ToList();

            return _mapper.Map<List<Edition>, List<EditionModel>>(orderItemList);
        }
        public async Task<PaymentModel> GetPaymentInOrderAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            OrderCheck(order);
            var payment = order.Payment;

            return _mapper.Map<Payments, PaymentModel>(payment);
        }


        public async Task RemoveItemFromOrderAsync(OrderItemModel orderItemModel)
        {
            var item = await _editionRepository.FindByIdAsync(orderItemModel.EditionId);
            var order = await _orderRepository.FindByIdAsync(orderItemModel.OrderId);

            await _orderRepository.RemoveItemToOrderAsync(order, item);
        }
        public async Task RemoveItemsListFromOrder(OrderItemsListModel orderItemsListModel)
        {
            var order = await _orderRepository.FindByIdAsync(orderItemsListModel.OrderId);
            OrderCheck(order);

            string[] editionsId = orderItemsListModel.Editions.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
            List<Edition> editionList = new List<Edition>();

            foreach (var edition in editionsId)
            {
                var tempEdition = await _editionRepository.FindByIdAsync(int.Parse(edition));

                if (tempEdition is null || tempEdition.IsRemoved)
                {
                    throw new CustomException($"Incorrect edition Id{edition}", HttpStatusCode.BadRequest);
                }

                editionList.Add(tempEdition);
            }

            await _orderRepository.RemoveItemListFromOrderAsync(order, editionList);
        }

        private void OrderCheck(Orders order)
        {
            if(order is null || order.IsRemoved)
            {
                throw new CustomException(ErrorConstant.NOTHING_EXIST, HttpStatusCode.BadRequest);
            }
        }
        private void OrderListCheck(List<Orders> query)
        {
            if (!query.Any())
                throw new CustomException(ErrorConstant.NOTHING_FOUND, HttpStatusCode.BadRequest);
        }
        private void PageModelValidation(PageModel pageModel)
        {
            if(pageModel.PageNumber is VariableConstant.EMPTY || pageModel.ElementsAmount is VariableConstant.EMPTY)
            {
                throw new CustomException("Incorrect page number or elements amount", HttpStatusCode.BadRequest);
            }
        }
    }
}
