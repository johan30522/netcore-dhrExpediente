using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using Serilog;

namespace AppExpedienteDHR.Core.Services
{
    public class ActionRuleWfService : IActionRuleWfService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public ActionRuleWfService(IContainerWork containerWork, IMapper mapper, ILogger logger)
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task CreateActionRule(ActionRuleWfViewModel actionRuleViewModel)
        {
            try
            {
                ActionRuleWf actionRule = _mapper.Map<ActionRuleWf>(actionRuleViewModel);
                await _containerWork.ActionRuleWf.Add(actionRule);
                await _containerWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating actionRule");
                throw;
            }
        }

        public async Task DeleteActionRule(int id)
        {
            try
            {
                ActionRuleWf actionRule = await _containerWork.ActionRuleWf.Get(id);
                if (actionRule == null)
                {
                    throw new Exception("ActionRule not found");
                }
                await _containerWork.ActionRuleWf.Remove(actionRule);
                await _containerWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting actionRule");
                throw;
            }
        }

        public async Task<ActionRuleWfViewModel> GetActionRule(int id)
        {
            try
            {
                ActionRuleWf actionRule = await _containerWork.ActionRuleWf.Get(id);
                ActionRuleWfViewModel actionRuleViewModel = _mapper.Map<ActionRuleWfViewModel>(actionRule);
                return actionRuleViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting actionRule");
                throw;
            }
        }

        public async Task<IEnumerable<ActionRuleWfViewModel>> GetActionRules()
        {
            try
            {
                IEnumerable<ActionRuleWf> actionRules = await _containerWork.ActionRuleWf.GetAll();
                IEnumerable<ActionRuleWfViewModel> actionRuleViewModels = _mapper.Map<IEnumerable<ActionRuleWfViewModel>>(actionRules);
                return actionRuleViewModels;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting actionRules");
                throw;
            }
        }

        public async Task UpdateActionRule(ActionRuleWfViewModel actionRuleViewModel)
        {
            try
            {
                ActionRuleWf actionRule = _mapper.Map<ActionRuleWf>(actionRuleViewModel);
                await _containerWork.ActionRuleWf.Update(actionRule);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating actionRule");
                throw;
            }
        }
    }
}
