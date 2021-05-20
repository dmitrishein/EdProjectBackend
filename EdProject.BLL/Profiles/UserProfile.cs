﻿using AutoMapper;
using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, User>();

            CreateMap<User,UserModel>()
                .ForMember(u => u.Fullname, opt=>opt.MapFrom(x => string.Format("{0} {1}",x.FirstName,x.LastName)));
        }
    }
}
