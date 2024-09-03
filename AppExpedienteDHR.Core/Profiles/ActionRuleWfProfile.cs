using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.Profiles
{
    public class ActionRuleWfProfile: Profile
    {
        public ActionRuleWfProfile()
        {
            CreateMap<ActionRuleWf, ActionRuleWfViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.ResultStateId, opt => opt.MapFrom(src => src.ResultStateId))
                .ForMember(dest => dest.RuleJson, opt => opt.MapFrom(src => src.RuleJson));

            CreateMap<ActionRuleWfViewModel, ActionRuleWf>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.ResultStateId, opt => opt.MapFrom(src => src.ResultStateId))
                .ForMember(dest => dest.RuleJson, opt => opt.MapFrom(src => src.RuleJson));
        }
    }
}
