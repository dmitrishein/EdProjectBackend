using AutoMapper;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Models;
using EdProject.DAL.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IOrdersService _orderService;
        public OrderController(IOrdersService orderService, IConfiguration configuration,IMapper mapper)
        {
            _orderService = orderService;
            _config = configuration;
        }

        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task<long> CreateOrder(OrderCreateModel orderCreateModel)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString();

            return await _orderService.CreateOrderAsync(token,orderCreateModel);
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

        [Authorize]
        [HttpPost("[action]")]
        public async Task<OrdersPageResponseModel> GetOrdersPage([FromBody]OrdersPageParameters pageModel)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString();
            var page = await _orderService.GetOrdersPageAsync(token,pageModel);
            return page ;
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



        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task UpdateOrderItem(OrderItemModel orderItem)
        {
            await _orderService.UpdateOrderItemAsync(orderItem);
        }

        [Authorize(Roles = "admin,client")]
        [HttpPost("[action]")]
        public async Task ClearOrder(long orderId)
        {
            await _orderService.ClearOrder(orderId);
        }


        [Authorize(Roles = "admin,client")]
        [HttpGet("[action]")]
        public async Task RemoveOrderById(long orderId)
        {
            await _orderService.RemoveOrderByIdAsync(orderId);
        }

    }
}
