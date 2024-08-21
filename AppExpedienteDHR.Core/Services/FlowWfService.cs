using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;   

namespace AppExpedienteDHR.Core.Services
{
    public class FlowWfService : IFlowWfService
    {

        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;

        public FlowWfService(IContainerWork containerWork, IMapper mapper)
        {
            _containerWork = containerWork;
            _mapper = mapper;
        }

        public async Task CreateFlow(FlowWfViewModel flowViewModel)
        {
            FlowWf flow = _mapper.Map<FlowWf>(flowViewModel);
            await _containerWork.FlowWf.Add(flow);
            await _containerWork.Save();
        }

        public async Task DeleteFlow(int id)
        {
            FlowWf flow = await _containerWork.FlowWf.Get(id);
            if (flow == null) {
                throw new Exception("Flow not found");
            }
            await _containerWork.FlowWf.Remove(flow);
            await _containerWork.Save();

        }

        public async Task<FlowWfViewModel> GetFlow(int id)
        {
            FlowWf flow = await _containerWork.FlowWf.Get(id);

            FlowWfViewModel flowViewModel = _mapper.Map<FlowWfViewModel>(flow);

            return flowViewModel;
        }

        public async Task<IEnumerable<FlowWfViewModel>> GetFlows()
        {
            IEnumerable<FlowWf> flows = await _containerWork.FlowWf.GetAll(includeProperties: "ActionGroupWf, ActionWf");

            IEnumerable<FlowWfViewModel> flowViewModels = _mapper.Map<IEnumerable<FlowWfViewModel>>(flows);

            return flowViewModels;

        }

        public async Task UpdateFlow(FlowWfViewModel flowViewModel)
        {
            FlowWf flow = _mapper.Map<FlowWf>(flowViewModel);
            await _containerWork.FlowWf.Update(flow);
            await _containerWork.Save();
        }
    }
}
