using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.Profiles
{
    public class ActionWfProfile : Profile
    {
        public ActionWfProfile() {
            CreateMap<ActionWf, ActionWfViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.NextStateId, opt => opt.MapFrom(src => src.NextStateId))
                .ForMember(dest => dest.EvaluationType, opt => opt.MapFrom(src => src.EvaluationType))
                .ForMember(dest => dest.Rules, opt => opt.MapFrom(src => src.ActionRules));


            CreateMap<ActionWfViewModel, ActionWf>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.NextStateId, opt => opt.MapFrom(src => src.NextStateId))
                .ForMember(dest => dest.EvaluationType, opt => opt.MapFrom(src => src.EvaluationType));
        }

    }
}
