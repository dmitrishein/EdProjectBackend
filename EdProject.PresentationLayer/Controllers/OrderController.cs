using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrdersService _orderService;
        public OrderController(IOrdersService orderService)
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
                StatusType = newOrder.StatusType
            };
            await _orderService.CreateOrderAsync(orderModel);
        }

        [HttpPost("[action]")]
        public async Task CreatePayment(PaymentViewModel newPayment )
        {
            StripeConfiguration.ApiKey = "sk_test_51IiJzdFHRC0nt9sgcxsXqaChnLHhiR1QJ2wMe2slbivpfEdGI7KMgYaOD2GUXrH3hJva9QdAqxBBNvoU5MUF4MFX00GVQiG0pp";

            // `source` is obtained with Stripe.js; see https://stripe.com/docs/payments/accept-a-payment-charges#web-create-token
            var options = new ChargeCreateOptions
            {
                Amount = 12200,
                Currency = "eur",
                Source = "tok_amex",
                Description = "Test Charge (created for API docs)",
            };
            var service = new ChargeService();
            service.Create(options);

            PaymentModel paymentModel = new()
            {
                TransactionId = options.Source,
            };

            await _orderService.CreatePaymentAsync(paymentModel);
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Orders>> GetOrdersByUserId(long userId)
        {
            return  await _orderService.GetOrdersListByUserIdAsync(userId);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<Orders>> GetOrders()
        {
            return await _orderService.GetOrdersListAsync();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Orders>> GetOrdersTest()
        {
            return await _orderService.GetOrdersListAsync();
        }


    }
}
