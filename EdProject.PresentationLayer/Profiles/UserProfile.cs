using AutoMapper;
using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;

namespace EdProject.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, AppUser>();
            CreateMap<UserCreateViewModel, UserCreateModel>();
            CreateMap<UserUpdViewModel, UserUpdateModel>();
        }
    }
}
