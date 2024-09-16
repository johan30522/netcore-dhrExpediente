using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using Serilog;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;

namespace AppExpedienteDHR.Core.Services.WorkFlow
{
    public class ActionWfService : IActionWfService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ActionWfService(IContainerWork containerWork, IMapper mapper, ILogger logger)
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task CreateAction(ActionWfViewModel actionViewModel)
        {
            try
            {
                StateWf state = await _containerWork.StateWf.Get(actionViewModel.StateId);
                if (state == null)
                {
                    throw new Exception("State not found");
                }

                ActionWf action = _mapper.Map<ActionWf>(actionViewModel);
                action.StateId = actionViewModel.StateId;

                action.State = state;

                // si tiene un grupo asociado en NextStateId lo agrega
                if (actionViewModel.NextStateId != null)
                {
                    StateWf nextState = await _containerWork.StateWf.Get(actionViewModel.NextStateId.Value);
                    if (nextState == null)
                    {
                        throw new Exception("Next State not found");
                    }
                    action.NextStateId = actionViewModel.NextStateId;
                    action.NextState = nextState;
                }


                await _containerWork.ActionWf.Add(action);
                await _containerWork.Save();

                // si tiene Grupos asociados los agrega
                if (actionViewModel.SelectedGroupIds.Any())
                {
                    foreach (var groupId in actionViewModel.SelectedGroupIds)
                    {
                        ActionGroupWf actionGroup = new ActionGroupWf
                        {
                            GroupId = groupId,
                            ActionId = action.Id
                        };
                        await _containerWork.ActionGroupWf.Add(actionGroup);
                    }
                    await _containerWork.Save();
                }




            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating action");
                throw;
            }

        }

        public async Task DeleteAction(int id)
        {
            try
            {
                ActionWf action = await _containerWork.ActionWf.Get(id);

                if (action == null)
                {
                    throw new Exception("Action not found");
                }
                action.IsDeleted = true;
                action.DeletedAt = DateTime.Now;
                await _containerWork.ActionWf.Update(action);
                await _containerWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting action");
                throw;
            }
        }

        public async Task<ActionWfViewModel> GetAction(int id)
        {
            try
            {

                ActionWf action = await _containerWork.ActionWf.GetFirstOrDefault(
                    a => a.Id == id && a.IsDeleted == false,
                    includeProperties: "ActionRules,ActionGroups.Group"
                 );
                ActionWfViewModel actionViewModel = _mapper.Map<ActionWfViewModel>(action);

                if (action.ActionGroups.Any())
                {
                    actionViewModel.SelectedGroupIds = action.ActionGroups.Select(ag => ag.GroupId).ToList();
                }

                return actionViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting action");
                throw;
            }
        }

        public async Task<IEnumerable<ActionWfViewModel>> GetActions(int stateId)
        {
            try
            {

                IEnumerable<ActionWf> actions = await _containerWork.ActionWf.GetAll(
                    a => a.StateId == stateId && a.IsDeleted == false);
                IEnumerable<ActionWfViewModel> actionViewModels = _mapper.Map<IEnumerable<ActionWfViewModel>>(actions);
                return actionViewModels;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting actions");
                throw;
            }
        }

        public async Task UpdateAction(ActionWfViewModel actionViewModel)
        {
            try
            {
                StateWf state = await _containerWork.StateWf.Get(actionViewModel.StateId);
                if (state == null)
                {
                    throw new Exception("State not found");
                }
                if (actionViewModel.NextStateId != null)
                {
                    StateWf nextState = await _containerWork.StateWf.Get(actionViewModel.NextStateId.Value);
                    if (nextState == null)
                    {
                        throw new Exception("Next State not found");
                    }
                }

                ActionWf action = _mapper.Map<ActionWf>(actionViewModel);

                await _containerWork.ActionWf.Update(action);
                await _containerWork.Save();


                // verifica si hubo cambios en los grupos asociados, si los hay los elimina y agrega los nuevos
                IEnumerable<ActionGroupWf> actionGroups = await _containerWork.ActionGroupWf.GetAll(ag => ag.ActionId == action.Id);
                if (actionGroups.Any())
                {
                    foreach (var actionGroup in actionGroups)
                    {
                        // elimina los grupos asociados que no estan en la lista de SelectedGroupIds
                        if (!actionViewModel.SelectedGroupIds.Contains(actionGroup.GroupId))
                        {
                            await _containerWork.ActionGroupWf.Remove(actionGroup);
                        }
                    }
                }
                // agrega los nuevos grupos asociados de la lista SelectedGroupIds solo si no existen
                foreach (var groupId in actionViewModel.SelectedGroupIds)
                {
                    if (!actionGroups.Any(ag => ag.GroupId == groupId))
                    {
                        ActionGroupWf actionGroup = new ActionGroupWf
                        {
                            GroupId = groupId,
                            ActionId = action.Id
                        };
                        await _containerWork.ActionGroupWf.Add(actionGroup);
                    }
                }

                await _containerWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating action");
                throw;
            }
        }
    }
}
