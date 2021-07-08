using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.BLL.Models.AuthorDTO;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorModel>();
            CreateMap<AuthorModel, Author>();
            CreateMap<Author, AuthorInEditionModel>().ReverseMap();
                                
        }
    }
}
