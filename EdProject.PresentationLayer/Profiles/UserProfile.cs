using AutoMapper;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, AppUser>();
        }
    }
}
