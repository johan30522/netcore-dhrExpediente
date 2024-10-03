using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.Profiles
{
    public class StateWfProfile: Profile
    {
        public StateWfProfile() {
            CreateMap<StateWf, StateWfViewModel>() //convierte de StateWf a StateWfViewModel
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.FlowWfId, opt => opt.MapFrom(src => src.FlowId))
                .ForMember(dest => dest.Actions, opt => opt.MapFrom(src => src.Actions))
                .ForMember(dest => dest.IsInitialState, opt => opt.MapFrom(src => src.IsInitialState))
                .ForMember(dest => dest.IsFinalState, opt => opt.MapFrom(src => src.IsFinalState));
            CreateMap<StateWfViewModel, StateWf>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.IsInitialState, opt => opt.MapFrom(src => src.IsInitialState))
                .ForMember(dest => dest.IsFinalState, opt => opt.MapFrom(src => src.IsFinalState));

        
        }
    }
}
