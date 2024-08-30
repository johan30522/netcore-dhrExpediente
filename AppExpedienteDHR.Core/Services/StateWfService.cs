using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using Serilog;

namespace AppExpedienteDHR.Core.Services
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
                await _containerWork.StateWf.Remove(state);
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
                //StateWf state = await _containerWork.StateWf.Get(id);
                // incluye las acciones asociadas al estado
                StateWf state = await _containerWork.StateWf.GetFirstOrDefault(
                    s => s.Id == id,
                    includeProperties: "Actions");
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
                IEnumerable<StateWf> states = await _containerWork.StateWf.GetAll(s => s.FlowId == flowId);
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
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating state");
                throw;
            }
        }
    }
}
