using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.Profiles
{
    public class StateWfProfile : Profile
    {
        public StateWfProfile()
        {
            CreateMap<StateWf, StateWfViewModel>() //convierte de StateWf a StateWfViewModel
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.FlowWfId, opt => opt.MapFrom(src => src.FlowId))
                .ForMember(dest => dest.Actions, opt => opt.MapFrom(src => src.Actions))
                .ForMember(dest => dest.IsInitialState, opt => opt.MapFrom(src => src.IsInitialState))
                .ForMember(dest => dest.IsFinalState, opt => opt.MapFrom(src => src.IsFinalState))
                .ForMember(dest => dest.IsNotificationActive, opt => opt.MapFrom(src => src.IsNotificationActive))
                .ForMember(dest => dest.StateNotification, opt => opt.MapFrom(src => src.StateNotification));
            CreateMap<StateWfViewModel, StateWf>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.IsInitialState, opt => opt.MapFrom(src => src.IsInitialState))
                .ForMember(dest => dest.IsFinalState, opt => opt.MapFrom(src => src.IsFinalState))
                .ForMember(dest => dest.IsNotificationActive, opt => opt.MapFrom(src => src.IsNotificationActive))
                .ForMember(dest => dest.StateNotification, opt => opt.MapFrom(src => src.StateNotification))
                .ForMember(dest => dest.Actions, opt => opt.MapFrom(src => src.Actions));

            // Map de StateNotificationWf a StateNotificationWfViewModel y viceversa
            CreateMap<StateNotificationWf, StateNotificationWfViewModel>()
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.To))
                .ForMember(dest => dest.Cc, opt => opt.MapFrom(src => src.Cc))
                .ForMember(dest => dest.Bcc, opt => opt.MapFrom(src => src.Bcc))
                 .ForMember(dest => dest.Groups, opt => opt.MapFrom(src =>
                    src.NotificationGroups.Select(ng => ng.Group))) // Lista completa para desplegar
                .ForMember(dest => dest.SelectedGroupIds, opt => opt.MapFrom(src =>
                    src.NotificationGroups.Select(ng => ng.GroupId))); // IDs seleccionados

            CreateMap<StateNotificationWfViewModel, StateNotificationWf>()
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.To))
                .ForMember(dest => dest.Cc, opt => opt.MapFrom(src => src.Cc))
                .ForMember(dest => dest.Bcc, opt => opt.MapFrom(src => src.Bcc))
                  .ForMember(dest => dest.NotificationGroups, opt => opt.MapFrom(src =>
                    src.SelectedGroupIds.Select(groupId => new NotificationGroupWf
                    {
                        GroupId = groupId,
                        NotificationId =src.StateId ?? 0
                    })));


        }
    }
}
