using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using Serilog;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AppExpedienteDHR.Core.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;


namespace AppExpedienteDHR.Core.Services.WorkFlow
{
    public class StateWfService : IStateWfService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;



        public StateWfService(IContainerWork containerWork, IMapper mapper, ILogger logger)
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task CreateState(StateWfViewModel stateViewModel, int flowId)
        {
            try
            {

                FlowWf flow = await _containerWork.FlowWf.Get(flowId);
                if (flow == null)
                {
                    throw new Exception("Flow not found");
                }


                StateWf state = _mapper.Map<StateWf>(stateViewModel);
                state.FlowId = flowId;
                state.Flow = flow;
                await _containerWork.StateWf.Add(state);
                await _containerWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating state");
                throw;
            }
        }

        public async Task DeleteState(int id)
        {
            try
            {
                StateWf state = await _containerWork.StateWf.Get(id);
                if (state == null)
                {
                    throw new Exception("State not found");
                }
                state.IsDeleted = true;
                state.DeletedAt = DateTime.Now;
                await _containerWork.StateWf.Update(state);
                await _containerWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting state");
                throw;
            }
        }

        public async Task<StateWfViewModel> GetState(int id)
        {
            try
            {

                StateWf state = await _containerWork.StateWf.GetFirstOrDefault(
                    s => s.Id == id && s.IsDeleted == false,
                    includeProperties: "Actions,StateNotification.NotificationGroups.Group");



                StateWfViewModel stateViewModel = _mapper.Map<StateWfViewModel>(state);
                return stateViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting state");
                throw;
            }
        }

        public async Task<IEnumerable<StateWfViewModel>> GetStates(int flowId)
        {
            try
            {
                IEnumerable<StateWf> states = await _containerWork.StateWf.GetAll(
                    s => s.FlowId == flowId && s.IsDeleted == false);
                IEnumerable<StateWfViewModel> stateViewModels = _mapper.Map<IEnumerable<StateWfViewModel>>(states);
                return stateViewModels;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting states");
                throw;
            }
        }

        public async Task UpdateState(StateWfViewModel stateViewModel)
        {
            try
            {
                StateWf state = _mapper.Map<StateWf>(stateViewModel);
                await _containerWork.StateWf.Update(state);

                await _containerWork.Save();

                // si el estado cuenta con una definicion de notificacion se actualiza
                if (state.IsNotificationActive)
                {

                    // si la notificacion no existe se crea, verifica si el estado tiene una notificacion asociada
                    StateNotificationWf stateNotification = await _containerWork.StateNotification.GetFirstOrDefault(
                        sn => sn.StateId == state.Id && sn.IsDeleted == false);
                    // crea la notificacion
                    int idStateNotification = 0;
                    StateNotificationWf newStateNotification = new StateNotificationWf
                    {
                        StateId = state.Id,
                        //State = state,
                        EmailTemplateId = stateViewModel.StateNotification.EmailTemplateId,
                        To = stateViewModel.StateNotification.To,
                        Cc = stateViewModel.StateNotification.Cc,
                        Bcc = stateViewModel.StateNotification.Bcc
                    };
                    if (stateNotification == null)
                    {
                        // si la notificacion no existe se crea
                        await _containerWork.StateNotification.Add(newStateNotification);
                        await _containerWork.Save();
                        idStateNotification = newStateNotification.Id;
                    }
                    else
                    {
                        // si la notificacion existe solo se actualiza
                        stateNotification.EmailTemplateId = newStateNotification.EmailTemplateId;
                        stateNotification.To = newStateNotification.To;
                        stateNotification.Cc = newStateNotification.Cc;
                        stateNotification.Bcc = newStateNotification.Bcc;
                        await _containerWork.StateNotification.Update(stateNotification);
                        await _containerWork.Save();
                        idStateNotification = stateNotification.Id;
                    }
                    // si tiene grupos de notificacion se actualizan, se eliminan los existentes y se crean los nuevos
                    IEnumerable<NotificationGroupWf> notificationGroups = await _containerWork.NotificationGroupWf.GetAll(
                        ng => ng.NotificationId == idStateNotification);
                    if (notificationGroups != null && notificationGroups.Count() > 0)
                    {
                        // se eliminan los grupos existentes
                        foreach (var notificationGroup in notificationGroups)
                        {
                            await _containerWork.NotificationGroupWf.Remove(notificationGroup);
                        }
                        await _containerWork.Save();
                    }
                    // se crean los nuevos grupos si existen
                    if (stateViewModel.StateNotification.SelectedGroupIds != null)
                    {
                        foreach (var groupId in stateViewModel.StateNotification.SelectedGroupIds)
                        {
                            NotificationGroupWf notificationGroup = new NotificationGroupWf
                            {
                                NotificationId = idStateNotification,
                                GroupId = groupId
                            };
                            await _containerWork.NotificationGroupWf.Add(notificationGroup);
                        }
                        await _containerWork.Save();
                    }

                }


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating state");
                throw;
            }
        }
    }
}
