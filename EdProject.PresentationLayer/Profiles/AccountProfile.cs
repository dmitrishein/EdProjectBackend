using AutoMapper;
using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;

namespace EdProject.BLL.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<UserCreateModel, AppUser>();
            CreateMap<RegisterViewModel, UserCreateModel>();
            CreateMap<LoginViewModel, UserSignInModel>();
        }
    }
}
