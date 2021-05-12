using AutoMapper;
using EdProject.BLL.Models.Payment;
using EdProject.DAL.Entities;

namespace EdProject.BLL.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentModel, Payments>();
            CreateMap<Payments, PaymentModel>();
        }
    }
}
