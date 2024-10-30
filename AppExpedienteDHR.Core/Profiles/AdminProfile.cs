using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.ViewModels.Admin;

namespace AppExpedienteDHR.Core.Profiles
{
    public class AdminProfile: Profile
    {
        public AdminProfile()
        {
            CreateMap<Derecho, DerechoViewModel>().ReverseMap();
            CreateMap<Descriptor, DescriptorViewModel>().ReverseMap();
            CreateMap<Especificidad, EspecificidadViewModel>().ReverseMap();
            CreateMap<Evento, EventoViewModel>().ReverseMap();

        }

    }
}
