using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Services
{
    public class ActionWfService : IActionWfService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;

        public ActionWfService(IContainerWork containerWork, IMapper mapper)
        {
            _containerWork = containerWork;
            _mapper = mapper;
        }

        public async Task CreateAction(ActionWfViewModel actionViewModel)
        {
            ActionWf action = _mapper.Map<ActionWf>(actionViewModel);
            await _containerWork.ActionWf.Add(action);
            await _containerWork.Save();

        }

        public async Task DeleteAction(int id)
            {
            ActionWf action = await _containerWork.ActionWf.Get(id);
            if (action == null)
            {
                throw new Exception("Action not found");
            }
            await _containerWork.ActionWf.Remove(action);
            await _containerWork.Save();
        }

        public async Task<ActionWfViewModel> GetAction(int id)
        {
            ActionWf action = await _containerWork.ActionWf.Get(id);
            ActionWfViewModel actionViewModel = _mapper.Map<ActionWfViewModel>(action);
            return actionViewModel;
        }

        public async Task<IEnumerable<ActionWfViewModel>> GetActions()
        {
            IEnumerable<ActionWf> actions = await _containerWork.ActionWf.GetAll();
            IEnumerable<ActionWfViewModel> actionViewModels = _mapper.Map<IEnumerable<ActionWfViewModel>>(actions);
            return actionViewModels;
        }

        public async Task UpdateAction(ActionWfViewModel actionViewModel)
        {
            ActionWf action = _mapper.Map<ActionWf>(actionViewModel);
            await _containerWork.ActionWf.Update(action);
        }
    }
}
