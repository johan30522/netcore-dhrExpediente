using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using Serilog;

namespace AppExpedienteDHR.Core.Services
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

                await _containerWork.ActionWf.Add(action);
                await _containerWork.Save();
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
                // selecciona solo los no eliminados IsDeleted
                ActionWf action = await _containerWork.ActionWf.GetFirstOrDefault(
                    a => a.Id == id && a.IsDeleted == false,
                    includeProperties: "ActionRules"
                 );
                ActionWfViewModel actionViewModel = _mapper.Map<ActionWfViewModel>(action);
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
                ActionWf action = _mapper.Map<ActionWf>(actionViewModel);
                await _containerWork.ActionWf.Update(action);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating action");
                throw;
            }
        }
    }
}
