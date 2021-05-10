using AutoMapper;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        IConfiguration _config;
        IOrdersService _orderService;
        IMapper _mapper;
        public OrderController(IOrdersService orderService, IConfiguration configuration,IMapper mapper)
        {
            _orderService = orderService;
            _config = configuration;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task CreateOrder(OrderModel newOrder)
        {
            await _orderService.CreateOrderAsync(newOrder);
        }
        [HttpPost("[action]")]
        public async Task CreateOrderItem(OrderItemModel newOrder)
        {
            await _orderService.CreateOrderItemAsync(newOrder);
        }

        [HttpPost("[action]")]
        public async Task CreatePayment(PaymentModel newPayment)
        {
             StripeConfiguration.ApiKey =_config["Stripe:SecretKey"];
             var options = new ChargeCreateOptions
             {
                 Amount = newPayment.Amount,
                 Currency = newPayment.Currency.ToString().ToLower(),
                 Source = "tok_amex",
                 Description = "Test Charge (created for API docs)",
             };
             var service = new ChargeService();
             service.Create(options);
             
             newPayment.TransactionId = options.Source;
             await _orderService.CreatePaymentAsync(newPayment);        
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public Task<List<OrderModel>> GetOrdersByUserId(long userId)
        {
            return  _orderService.GetOrdersByUserId(userId);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public Task<List<OrderModel>> GetOrders()
        {
            return _orderService.GetOrdersList();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public Task<OrderModel> GetOrderById(long orderId)
        {
            return _orderService.GetOrderById(orderId);
        }


    }
}
