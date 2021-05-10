using AutoMapper;
using EdProject.BLL.Models.Orders;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Orders, OrderModel>();
            CreateMap<OrderModel, Orders>();
        }
    }
}
