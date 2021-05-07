using AutoMapper;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.Payment;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;
using System.Collections.Generic;

namespace EdProject.BLL.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Orders, OrderModel>();
            CreateMap<OrderModel, Orders>();
            CreateMap<OrderViewModel, OrderModel>();
            CreateMap<OrderItemViewModel, OrderItemModel>();
            CreateMap<PaymentViewModel, PaymentModel>();
        }
    }
}
