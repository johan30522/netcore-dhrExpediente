using System;
using System.Collections.Generic;
using AppExpedienteDHR.Core.Domain.IdentityEntities;
using AppExpedienteDHR.Core.ViewModels.Role;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AppExpedienteDHR.Core.Profiles
{
    public class RoleProfile: Profile   
    {
        public RoleProfile() {
            // Configuración del mapeo de ApplicationUserRole a RoleViewModel
            CreateMap<ApplicationUserRole, RoleViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<RoleViewModel, IdentityRole>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }

         
        

    }
}
