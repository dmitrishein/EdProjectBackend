using AutoMapper;
using EdProject.BLL;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Entities.Enums;
using EdProject.PresentationLayer.Middleware;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task CreateOrder(OrderViewModel newOrder)
        {
            await _orderService.CreateOrderAsync(_mapper.Map<OrderViewModel,OrderModel>(newOrder));
        }
        [HttpPost("[action]")]
        public async Task CreateOrderItem(OrderItemViewModel newOrder)
        {
            await _orderService.CreateOrderItemAsync(_mapper.Map<OrderItemViewModel, OrderItemModel>(newOrder));
        }

        [HttpPost("[action]")]
        public async Task CreatePayment(PaymentViewModel newPayment)
        {
             StripeConfiguration.ApiKey =_config["Stripe:SecretKey"];
             var options = new ChargeCreateOptions
             {
                 Amount = newPayment.Amount,
                 Currency = newPayment.currency.ToString().ToLower(),
                 Source = "tok_amex",
                 Description = "Test Charge (created for API docs)",
             };
             var service = new ChargeService();
             service.Create(options);
             
             var newPaymentModel = _mapper.Map<PaymentViewModel, PaymentModel>(newPayment);
             newPaymentModel.TransactionId = options.Source;
             await _orderService.CreatePaymentAsync(newPaymentModel);        
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
