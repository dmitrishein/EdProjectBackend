using AutoMapper;
using EdProject.BLL.Models.Author;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;

namespace EdProject.BLL.Profiles
{
    public class AuthorInEditionProfile : Profile
    {
        public AuthorInEditionProfile()
        {
            CreateMap<AuthorInEditions, AuthorInEditionModel>();
            CreateMap<AuthorInEditionModel, AuthorInEditions>();
            CreateMap<AuthorInEditionViewModel, AuthorInEditionModel>();
        }
    }
}
