using AutoMapper;
using EdProject.BLL.Common.Options;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.BLL.Models.User;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Entities.Enums;
using EdProject.DAL.Enums;
using EdProject.DAL.Models;
using EdProject.DAL.Pagination.Models;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using OrderItem = EdProject.DAL.Entities.OrderItem;

namespace EdProject.BLL.Services
{
    public class OrderService : IOrdersService
    {
        IOrderRepository _orderRepository;
        IPaymentRepository _paymentRepository;
        IEditionRepository _editionRepository;
        IMapper _mapper;
        private readonly StripeOptions _conectStripeOption;

        public OrderService(IMapper mapper, IOptions<StripeOptions> options,IOrderRepository orderRepository,
                            IPaymentRepository paymentRepository,IEditionRepository editionRepository)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _editionRepository = editionRepository;
            _mapper = mapper;
            _conectStripeOption = options.Value;
        }

        public async Task<long> CreateOrderAsync(string token, OrderCreateModel orderCreateModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer ", string.Empty);
            var userToken = tokenHandler.ReadJwtToken(token);
            var userId = userToken.Claims.First(claim => claim.Type == "id").Value;

            var orderItems = _mapper.Map<List<OrderItem>>(orderCreateModel.OrderItems);

            Orders userOrder = new Orders
            {
                OrderItems = orderItems,
                UserId = Convert.ToInt64(userId)
            };
            await _orderRepository.CreateAsync(userOrder);

            var Editions = await _editionRepository.GetEditionRangeAsync(orderCreateModel.OrderItems.Select(x => x.EditionId).ToList());
            userOrder.Editions = Editions;
            var orderPrice = userOrder.Editions.Sum(x => x.Price * userOrder.OrderItems.Find(y => y.EditionId == x.Id).ItemsCount);

            StripeConfiguration.ApiKey = _conectStripeOption.SecretKey;
            var options = new ChargeCreateOptions
            {
                Amount = (long)(orderPrice * VariableConstant.CONVERT_TO_CENT_VALUE),
                Currency = CurrencyTypes.USD.ToString().ToLower(),
                Description = $"Order #{userOrder.Id}",
                Source = orderCreateModel.SourceId
            };

            try
            {
                var service = new ChargeService();
                var charge = service.Create(options);
                Payments newPayment = new Payments
                {
                    TransactionId = charge.Id,
                    Amount = orderPrice,
                    Currency = CurrencyTypes.USD
                };
                await _paymentRepository.CreateAsync(newPayment);


                userOrder.StatusType = PaidStatusType.Paid;
                userOrder.Payment = newPayment;
                userOrder.Description = $"OrderId:{userOrder.Id}| PaymentId : {newPayment.Id}";
                userOrder.Total = orderPrice;
            }
            catch (Exception x)
            {
               userOrder.Description = PaidStatusType.Unpaid.ToString();
               userOrder.StatusType = PaidStatusType.Unpaid;
               userOrder.Total = orderPrice;
               throw new CustomException($"{ErrorConstant.UNSUCCESSFUL_PAYMENT}, {x.Message}", HttpStatusCode.BadRequest);
            }
            finally
            {
              await _orderRepository.SaveChangesAsync();
            }
            return userOrder.Id;
        }
        public async Task UpdateOrderAsync(string token, OrderUpdateModel orderUpdate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer ", string.Empty);
            var userToken = tokenHandler.ReadJwtToken(token);
            var userId = userToken.Claims.First(claim => claim.Type == "id").Value;

            var updateOrder = await _orderRepository.FindByIdAsync(orderUpdate.OrderId);
            if(!updateOrder.UserId.ToString().Equals(userId))
            {
                throw new CustomException(ErrorConstant.INCORRECT_ORDER, HttpStatusCode.BadRequest);
            }

            var orderPrice = updateOrder.OrderItems.Sum(x => x.Edition.Price * x.ItemsCount);

            StripeConfiguration.ApiKey = _conectStripeOption.SecretKey;
            var options = new ChargeCreateOptions
            {
                Amount = (long)(orderPrice * VariableConstant.CONVERT_TO_CENT_VALUE),
                Currency = CurrencyTypes.USD.ToString().ToLower(),
                Description = $"Order #{updateOrder.Id}",
                Source = orderUpdate.SourceId
            };

            try
            {
                var service = new ChargeService();
                var charge = service.Create(options);
                Payments newPayment = new Payments
                {
                    TransactionId = charge.Id,
                    Amount = orderPrice,
                    Currency = CurrencyTypes.USD
                };
                await _paymentRepository.CreateAsync(newPayment);


                updateOrder.StatusType = PaidStatusType.Paid;
                updateOrder.Payment = newPayment;
                updateOrder.Description = $"OrderId:{updateOrder.Id}| PaymentId : {newPayment.Id}";
                updateOrder.Total = orderPrice;
            }
            catch (Exception x)
            {
                updateOrder.Description = PaidStatusType.Unpaid.ToString();
                updateOrder.StatusType = PaidStatusType.Unpaid;
                updateOrder.Total = orderPrice;
                throw new CustomException($"{ErrorConstant.UNSUCCESSFUL_PAYMENT}, {x.Message}", HttpStatusCode.BadRequest);
            }
            finally
            {
                await _orderRepository.SaveChangesAsync();
            }

        }
        public async Task<OrdersPageResponseModel> GetOrdersPageAsync(string token,OrdersPageParameters pageParams)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer ", string.Empty);
            var userToken = tokenHandler.ReadJwtToken(token);
            var userId = userToken.Claims.First(claim => claim.Type == "id").Value;
            pageParams.UserId = userId;
            var resultPage = await _orderRepository.OrdersPage(pageParams);
            var lis = _mapper.Map<OrdersPageResponseModel>(resultPage);
            return lis;
        }

        //for admin
        public async Task RemoveOrderByIdAsync(long orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            order.IsRemoved = true;
            await _orderRepository.UpdateAsync(order);
        }
       
    }
}
