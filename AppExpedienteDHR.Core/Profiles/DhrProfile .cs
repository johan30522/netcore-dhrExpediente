﻿using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;

namespace AppExpedienteDHR.Core.Profiles
{
    public class DhrProfile : Profile
    {
        public DhrProfile() {

            // convierte de Denuncia a DenunciaViewModel
            CreateMap<Denuncia, DenunciaViewModel>()
                .ForMember(dest => dest.Denunciante, opt => opt.MapFrom(src => src.Denunciante))
                .ForMember(dest => dest.PersonaAfectada, opt => opt.MapFrom(src => src.PersonaAfectada))
                .ForMember(dest => dest.DenunciaAdjuntos, opt => opt.MapFrom(src => src.DenunciaAdjuntos))
                .ReverseMap();
            CreateMap<Denunciante, DenuncianteViewModel>().ReverseMap();
            CreateMap<DenunciaAdjunto, DenunciaAdjuntoViewModel>()
            .ForMember(dest => dest.RutaArchivo, opt => opt.MapFrom(src => src.Adjunto.Ruta))
            .ForMember(dest => dest.NombreArchivo, opt => opt.MapFrom(src => src.Adjunto.NombreOriginal))
            .ForMember(dest => dest.FechaSubida, opt => opt.MapFrom(src => src.Adjunto.FechaSubida))
            .ReverseMap();

            // Convierte de Expediente a ExpedienteViewModel
            CreateMap<Expediente, ExpedienteViewModel>()
                .ForMember(dest => dest.Denuncia, opt => opt.MapFrom(src => src.Denuncia))
                .ForMember(dest => dest.PersonaAfectada, opt => opt.MapFrom(src => src.PersonaAfectada))    
                .ForMember(dest => dest.Denunciante, opt => opt.MapFrom(src => src.Denunciante))
                .ForMember(dest => dest.ExpedienteAdjuntos, opt => opt.MapFrom(src => src.ExpedienteAdjuntos))
                .ReverseMap();

            CreateMap<ExpedienteAdjunto, ExpedienteAdjuntoViewModel>()
            .ForMember(dest => dest.RutaArchivo, opt => opt.MapFrom(src => src.Adjunto.Ruta))
            .ForMember(dest => dest.NombreArchivo, opt => opt.MapFrom(src => src.Adjunto.NombreOriginal))
            .ForMember(dest => dest.FechaSubida, opt => opt.MapFrom(src => src.Adjunto.FechaSubida))
            .ReverseMap();


            // reverse map
            CreateMap<Expediente, ExpedienteViewModel>().ReverseMap();
            CreateMap<PersonaAfectada, PersonaAfectadaViewModel>().ReverseMap();

            CreateMap<DenunciaViewModel, Denuncia>().ReverseMap();
            CreateMap<DenuncianteViewModel, Denunciante>().ReverseMap();
            CreateMap<ExpedienteViewModel, Expediente>().ReverseMap();
            CreateMap<PersonaAfectadaViewModel, PersonaAfectada>().ReverseMap();
            CreateMap<Adjunto, AdjuntoViewModel>().ReverseMap();




            CreateMap<Denuncia, DenunciaItemListViewModel>()
                //concatena el nombre y PrimerApellido y SegundoApellido
                .ForMember(dest => dest.DenuncianteNombre, opt => opt.MapFrom(src => src.Denunciante.Nombre + " " + src.Denunciante.PrimerApellido + " " + src.Denunciante.SegundoApellido))
                .ForMember(dest => dest.DetalleDenuncia, opt => opt.MapFrom(src => src.DetalleDenuncia))
                .ForMember(dest => dest.Petitoria, opt => opt.MapFrom(src => src.Petitoria))
                .ForMember(dest => dest.FechaDenuncia, opt => opt.MapFrom(src => src.FechaDenuncia));

            CreateMap<Expediente, ExpedienteItemListViewModel>()
                //concatena el nombre y PrimerApellido y SegundoApellido
                //.ForMember(dest => dest.DenuncianteNombre, opt => opt.MapFrom(src => src.Denunciante.Nombre + " " + src.Denunciante.PrimerApellido + " " + src.Denunciante.SegundoApellido))
                .ForMember(dest => dest.ExpedienteDetalle, opt => opt.MapFrom(src => src.Detalle))
                .ForMember(dest => dest.ExpedienteFechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion));
        }
    }
}
