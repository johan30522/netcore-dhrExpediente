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
                ActionWf action = _mapper.Map<ActionWf>(actionViewModel);
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
                await _containerWork.ActionWf.Remove(action);
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
                ActionWf action = await _containerWork.ActionWf.Get(id);
                ActionWfViewModel actionViewModel = _mapper.Map<ActionWfViewModel>(action);
                return actionViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting action");
                throw;
            }
        }

        public async Task<IEnumerable<ActionWfViewModel>> GetActions()
        {
            try
            {
                IEnumerable<ActionWf> actions = await _containerWork.ActionWf.GetAll();
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
