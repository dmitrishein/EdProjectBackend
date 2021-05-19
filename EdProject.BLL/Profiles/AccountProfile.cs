using AutoMapper;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<UserCreateModel, User>().ReverseMap();

        }
    }
}
