using AutoMapper;
using EdProject.BLL.Models.Orders;
using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using EdProject.DAL.Pagination.Models.Order;

namespace EdProject.BLL.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Orders, OrderModel>();
            CreateMap<OrderModel, Orders>();
            CreateMap<OrderItemModel, OrderItem>().ReverseMap()
                .ForMember(itemModel => itemModel.Title, entity => entity.MapFrom(edit => edit.Edition.Title))
                .ForMember(itemModel => itemModel.BookType, entity => entity.MapFrom(edit => edit.Edition.Type));
            CreateMap<OrderPageModel, OrdersPageResponseModel>().ReverseMap();

        }
    }
}
