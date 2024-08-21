using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Services
{
    public class ActionRuleWfService : IActionRuleWfService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;


        public ActionRuleWfService(IContainerWork containerWork, IMapper mapper)
        {
            _containerWork = containerWork;
            _mapper = mapper;
        }


        public async Task CreateActionRule(ActionRuleWfViewModel actionRuleViewModel)
        {
            ActionRuleWf actionRule = _mapper.Map<ActionRuleWf>(actionRuleViewModel);
            await _containerWork.ActionRuleWf.Add(actionRule);
            await _containerWork.Save();
        }

        public async Task DeleteActionRule(int id)
        {
            ActionRuleWf actionRule = await _containerWork.ActionRuleWf.Get(id);
            if (actionRule == null)
            {
                throw new Exception("ActionRule not found");
            }
            await _containerWork.ActionRuleWf.Remove(actionRule);
            await _containerWork.Save();
        }

        public async Task<ActionRuleWfViewModel> GetActionRule(int id)
        {
            ActionRuleWf actionRule = await _containerWork.ActionRuleWf.Get(id);
            ActionRuleWfViewModel actionRuleViewModel = _mapper.Map<ActionRuleWfViewModel>(actionRule);
            return actionRuleViewModel;
        }

        public async Task<IEnumerable<ActionRuleWfViewModel>> GetActionRules()
        {
            IEnumerable<ActionRuleWf> actionRules = await _containerWork.ActionRuleWf.GetAll();
            IEnumerable<ActionRuleWfViewModel> actionRuleViewModels = _mapper.Map<IEnumerable<ActionRuleWfViewModel>>(actionRules);
            return actionRuleViewModels;
        }

        public async Task UpdateActionRule(ActionRuleWfViewModel actionRuleViewModel)
        {
            ActionRuleWf actionRule = _mapper.Map<ActionRuleWf>(actionRuleViewModel);
            await _containerWork.ActionRuleWf.Update(actionRule);
        }
    }
}
