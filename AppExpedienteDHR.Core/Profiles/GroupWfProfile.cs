using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;

namespace AppExpedienteDHR.Core.Profiles
{
    public class GroupWfProfile: Profile
    {
        public GroupWfProfile()
        {
            CreateMap<GroupWf, GroupWfViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.FlowWfId, opt => opt.MapFrom(src => src.FlowId))
                 .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.GroupUsers.Select(gu => gu.User)));

            CreateMap<GroupWfViewModel, GroupWf>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.FlowId, opt => opt.MapFrom(src => src.FlowWfId));
        }

    }
}
