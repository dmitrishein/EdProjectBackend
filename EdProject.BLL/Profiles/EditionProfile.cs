using AutoMapper;
using EdProject.BLL.Models.Editions;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class EditionProfile : Profile
    {
        public EditionProfile()
        {
            CreateMap<Edition, EditionModel>();
            CreateMap<Edition, CreateEditionModel>().ReverseMap();
            CreateMap<EditionModel, Edition>();
            CreateMap<EditionPageModel, EditionPageResponseModel>().ReverseMap();
        }
    }
}
