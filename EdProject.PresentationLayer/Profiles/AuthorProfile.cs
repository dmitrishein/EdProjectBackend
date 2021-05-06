using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;

namespace EdProject.BLL.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorModel>();
            CreateMap<AuthorModel, Author>();
            CreateMap<AuthorViewModel, AuthorModel>();
        }
    }
}
