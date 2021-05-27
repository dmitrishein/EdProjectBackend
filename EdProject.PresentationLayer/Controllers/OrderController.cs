using AutoMapper;
using EdProject.BLL.Models.Base;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EdProject.BLL.Models.User;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        IConfiguration _config;
        IOrdersService _orderService;
        public OrderController(IOrdersService orderService, IConfiguration configuration,IMapper mapper)
        {
            _orderService = orderService;
            _config = configuration;
        }

        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task CreateOrder(OrderModel newOrder)
        {
            await _orderService.CreateOrderAsync(newOrder);
        }

        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task CreateOrderItem(OrderItemModel newOrder)
        {
            await _orderService.CreateItemInOrderAsync(newOrder);
        }

        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task CreateOrderItemList(List<OrderItemModel> newOrder)
        {
            await _orderService.CreateItemsListInOrderAsync(newOrder);
        }


        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task CreatePayment(PaymentModel newPayment)
        {
             await _orderService.CreatePaymentAsync(newPayment);        
        }


       
        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public Task<List<OrderModel>> GetOrdersByUserId(long userId)
        {
            return  _orderService.GetOrdersByUserIdAsync(userId);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task<List<OrderModel>> GetOrdersList()
        {
            return await _orderService.GetOrdersListAsync();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public Task<List<OrderModel>> GetOrdersPage(FilterPageModel pageModel)
        {
            return _orderService.GetOrdersPageAsync(pageModel);
        }


        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public Task<OrderModel> GetOrderById(long orderId)
        {
            return _orderService.GetOrderByIdAsync(orderId);
        }


        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public async Task<List<OrderItemModel>> GetOrderedItemsByOrder(long orderId)
        {
            return await _orderService.GetItemsInOrderAsync(orderId);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task UpdateOrderItem(UpdateOrderItem orderItem)
        {
            await _orderService.UpdateOrderItemAsync(orderItem);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task<PaymentModel> GetPaymentInOrder(long orderId)
        {
            return await _orderService.GetPaymentInOrderAsync(orderId);
        }

        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task ClearOrder(long orderId)
        {
            await _orderService.ClearOrder(orderId);
        }

        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task RemoveItemFromOrder(OrderItemModel orderItemModel)
        {
           await _orderService.RemoveItemFromOrderAsync(orderItemModel);
        }

        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task RemoveItemsListFromOrder(List<OrderItemModel> orderItemListModel)
        {
            await _orderService.RemoveItemsListFromOrder(orderItemListModel);
        }

        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public async Task RemoveOrderById(long orderId)
        {
            await _orderService.RemoveOrderByIdAsync(orderId);
        }

    }
}
