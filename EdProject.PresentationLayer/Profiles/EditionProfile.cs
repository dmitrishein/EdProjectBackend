using AutoMapper;
using EdProject.BLL.Models.PrintingEditions;
using EdProject.DAL.Entities;
using EdProject.PresentationLayer.Models;

namespace EdProject.BLL.Profiles
{
    public class EditionProfile : Profile
    {
        public EditionProfile()
        {
            CreateMap<Edition, PrintingEditionModel>();
            CreateMap<PrintingEditionModel, Edition>();
            CreateMap<PrintingEditionViewModel, PrintingEditionModel>();
        }
    }
}
