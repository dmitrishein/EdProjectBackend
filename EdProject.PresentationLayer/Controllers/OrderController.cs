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

        [Authorize]
        [HttpPost("[action]")]
        public async Task<long> CreateOrder(OrderCreateModel orderCreateModel)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString();

            return await _orderService.CreateOrderAsync(token,orderCreateModel);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task UpdateOrder (OrderUpdateModel orderCreateModel)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString();
            await _orderService.UpdateOrderAsync(token, orderCreateModel);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<OrdersPageResponseModel> GetOrdersPage([FromBody]OrdersPageParameters pageModel)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString();
            var page = await _orderService.GetOrdersPageAsync(token,pageModel);
            return page ;
        }


    }
}
