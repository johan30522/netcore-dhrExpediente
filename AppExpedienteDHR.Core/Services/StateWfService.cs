using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;

namespace AppExpedienteDHR.Core.Services
{
    public class StateWfService : IStateWfService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        public StateWfService(IContainerWork containerWork, IMapper mapper)
        {
            _containerWork = containerWork;
            _mapper = mapper;
        }
        public async Task CreateState(StateWfViewModel stateViewModel)
        {
            StateWf state = _mapper.Map<StateWf>(stateViewModel);
            await _containerWork.StateWf.Add(state);
            await _containerWork.Save();
        }

        public async Task DeleteState(int id)
        {
            StateWf state = await _containerWork.StateWf.Get(id);
            if (state == null)
            {
                throw new Exception("State not found");
            }
            await _containerWork.StateWf.Remove(state);
            await _containerWork.Save();
        }

        public async Task<StateWfViewModel> GetState(int id)
        {
            StateWf state = await _containerWork.StateWf.Get(id);
            StateWfViewModel stateViewModel = _mapper.Map<StateWfViewModel>(state);
            return stateViewModel;
        }

        public async Task<IEnumerable<StateWfViewModel>> GetStates()
        {
            IEnumerable<StateWf> states = await _containerWork.StateWf.GetAll();
            IEnumerable<StateWfViewModel> stateViewModels = _mapper.Map<IEnumerable<StateWfViewModel>>(states);
            return stateViewModels;
        }

        public async Task UpdateState(StateWfViewModel stateViewModel)
        {
            StateWf state = _mapper.Map<StateWf>(stateViewModel);
            await _containerWork.StateWf.Update(state);
        }
    }
}
