﻿using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;

namespace AppExpedienteDHR.Core.Profiles
{
    public class DhrProfile : Profile
    {
        public DhrProfile() {
            CreateMap<Denuncia, DenunciaViewModel>().ReverseMap();
            CreateMap<Denunciante, DenuncianteViewModel>().ReverseMap();
            CreateMap<DenunciaAdjunto, DenunciaAdjuntoViewModel>().ReverseMap();
            CreateMap<Expediente, ExpedienteViewModel>().ReverseMap();
            CreateMap<PersonaAfectada, PersonaAfectadaViewModel>().ReverseMap();

            // reverse map
            CreateMap<DenunciaViewModel, Denuncia>().ReverseMap();
            CreateMap<DenuncianteViewModel, Denunciante>().ReverseMap();
            CreateMap<DenunciaAdjuntoViewModel, DenunciaAdjunto>().ReverseMap();
            CreateMap<ExpedienteViewModel, Expediente>().ReverseMap();
            CreateMap<PersonaAfectadaViewModel, PersonaAfectada>().ReverseMap();

            CreateMap<Denuncia, DenunciaListadoViewModel>()
                //concatena el nombre y PrimerApellido y SegundoApellido
                .ForMember(dest => dest.DenuncianteNombre, opt => opt.MapFrom(src => src.Denunciante.Nombre + " " + src.Denunciante.PrimerApellido + " " + src.Denunciante.SegundoApellido))
                .ForMember(dest => dest.DetalleDenuncia, opt => opt.MapFrom(src => src.DetalleDenuncia))
                .ForMember(dest => dest.Petitoria, opt => opt.MapFrom(src => src.Petitoria))
                .ForMember(dest => dest.FechaDenuncia, opt => opt.MapFrom(src => src.FechaDenuncia));
        }
    }
}
