using AutoMapper;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class EditionProfile : Profile
    {
        public EditionProfile()
        {
            CreateMap<Edition, PrintingEditionModel>();
            CreateMap<PrintingEditionModel, Edition>();
        }
    }
}
