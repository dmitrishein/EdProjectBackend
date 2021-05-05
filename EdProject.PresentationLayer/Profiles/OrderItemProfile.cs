using AutoMapper;
using EdProject.BLL.Models.Orders;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItemModel, OrderItems>();
        }
    }
}
