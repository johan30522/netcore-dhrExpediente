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
                .ForMember(dest => dest.EvaluationType, opt => opt.MapFrom(src => src.EvaluationType));


            CreateMap<ActionWfViewModel, ActionWf>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.EvaluationType, opt => opt.MapFrom(src => src.EvaluationType));
        }

    }
}
