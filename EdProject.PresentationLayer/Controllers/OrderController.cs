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
        public OrderController(IOrdersService orderService, IConfiguration configuration)
        {
            _orderService = orderService;
            _config = configuration;
        }

        [HttpPost("[action]")]
        public async Task CreateOrder(OrderViewModel newOrder)
        {
            OrderModel orderModel = new()
            {
                UserId = newOrder.UserId,
                Description = newOrder.Description,
                Date = DateTime.Now,
                StatusType = newOrder.StatusType
            };
           
            await _orderService.CreateOrderAsync(orderModel);
        }
        [HttpPost("[action]")]
        public async Task CreateOrderItem(OrderItemViewModel newOrder)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemViewModel, OrderItemModel>());
            var _mapper = new Mapper(config);
            var orderitemModel = _mapper.Map<OrderItemViewModel, OrderItemModel>(newOrder);

            await _orderService.CreateOrderItemAsync(orderitemModel);
        }

        [HttpPost("[action]")]
        public async Task CreatePayment(PaymentViewModel newPayment)
        {
            try
            {
                StripeConfiguration.ApiKey =_config["Stripe:SecretKey"];

                 var currency = (int)newPayment.currency;
                 var options = new ChargeCreateOptions
            {
                Amount = newPayment.Amount,
                Currency = newPayment.currency.ToString().ToLower(),
                Source = "tok_amex",
                Description = "Test Charge (created for API docs)",
            };
                 var service = new ChargeService();
                 service.Create(options);
                 var config = new MapperConfiguration(cfg => cfg.CreateMap<PaymentViewModel, PaymentModel>());
                 var _mapper = new Mapper(config);
                 var newPaymentModel = _mapper.Map<PaymentViewModel, PaymentModel>(newPayment);
                 newPaymentModel.TransactionId = options.Source;
                 await _orderService.CreatePaymentAsync(newPaymentModel);
            }
            catch(CustomException x)
            {
                throw new CustomException($"{x.Message}", 400);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public List<OrderModel> GetOrdersByUserId(long userId)
        {
            return  _orderService.GetOrdersByUserId(userId);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public List<OrderModel> GetOrders()
        {
            return  _orderService.GetOrdersList();
        }

      


    }
}
