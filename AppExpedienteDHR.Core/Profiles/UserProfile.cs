using AutoMapper;
using AppExpedienteDHR.Core.Domain.IdentityEntities;
using AppExpedienteDHR.Core.ViewModels.User;
using AppExpedienteDHR.Core.Resolver;


namespace AppExpedienteDHR.Core.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile() {
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                //.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name)));
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles)); // Mapeo de roles



            CreateMap<UserViewModel, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position));
        
        }
       
    }
}
