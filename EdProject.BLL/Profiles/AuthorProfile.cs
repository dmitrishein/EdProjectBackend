using AutoMapper;
using EdProject.BLL.Models.AuthorDTO;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorModel>()
                .ForMember(itemModel => itemModel.EditionsList, entity => entity.MapFrom(edit => edit.Editions));
            CreateMap<AuthorModel, Author>();
                                
        }
    }
}
