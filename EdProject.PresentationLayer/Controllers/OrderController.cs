using EdProject.BLL.Models.Orders;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("[action]")]
        public async Task CreateOrder(OrderViewModel newOrder)
        {

            OrderModel orderModel = new()
            {
                UserId = newOrder.UserId,
                Description = newOrder.Description,
                Date = DateTime.Now,
                PaymentId = newOrder.PaymentId,
                StatusType = newOrder.StatusType
            };

            await _orderService.CreateOrderAsync(orderModel);
        }

        [HttpPost("[action]")]
        public async Task CreateOrderItem(OrderItemViewModel newOrder)
        {

            OrderItemModel orderItemModel = new()
            {
                EditionId = newOrder.EditionId,
                Amount = newOrder.Amount,
                Currency = newOrder.Currency,
                OrderId = newOrder.OrderId

            };

            await _orderService.CreateOrderItemAsync(orderItemModel);
        }

        [HttpGet("[action]")]
        public IQueryable<Orders> GetOrdersByUserId(long userId)
        {
            return _orderService.GetOrdersListByUserId(userId);
        }


    }
}
