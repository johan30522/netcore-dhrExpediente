using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using Serilog;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;

namespace AppExpedienteDHR.Core.Services.WorkFlow
{
    public class FlowWfService : IFlowWfService
    {

        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FlowWfService(IContainerWork containerWork, IMapper mapper, ILogger logger)
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<FlowWfViewModel> CreateFlow(FlowWfViewModel flowViewModel)
        {
            try
            {
                FlowWf flow = _mapper.Map<FlowWf>(flowViewModel);
                await _containerWork.FlowWf.Add(flow);
                await _containerWork.Save();
                FlowWfViewModel flowViewModelRes = _mapper.Map<FlowWfViewModel>(flow);
                return flowViewModelRes;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating flow");
                throw;
            }
        }

        public async Task DeleteFlow(int id)
        {
            try
            {

                FlowWf flow = await _containerWork.FlowWf.Get(id);
                if (flow == null)
                {
                    throw new Exception("Flow not found");
                }
                //await _containerWork.FlowWf.Remove(flow);
                flow.IsDeleted = true;
                flow.DeletedAt = DateTime.Now;
                await _containerWork.FlowWf.Update(flow);
                await _containerWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting flow");
                throw;
            }

        }

        public async Task<FlowWfViewModel> GetFlow(int id)
        {
            try
            {
                FlowWf flow = await _containerWork.FlowWf.GetFirstOrDefault(
                    flow => flow.Id == id && flow.IsDeleted == false
                );

                FlowWfViewModel flowViewModel = _mapper.Map<FlowWfViewModel>(flow);

                return flowViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting flow");
                throw;
            }
        }

        public async Task<IEnumerable<FlowWfViewModel>> GetFlows()
        {
            try
            {

                IEnumerable<FlowWf> flows = await _containerWork.FlowWf.GetAll(
                    flow => flow.IsDeleted == false
                );

                IEnumerable<FlowWfViewModel> flowViewModels = _mapper.Map<IEnumerable<FlowWfViewModel>>(flows);

                return flowViewModels;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting flows");
                throw;
            }

        }

        public async Task<FlowWfViewModel> UpdateFlow(FlowWfViewModel flowViewModel)
        {
            try
            {
                FlowWf flow = _mapper.Map<FlowWf>(flowViewModel);
                await _containerWork.FlowWf.Update(flow);
                await _containerWork.Save();
                FlowWfViewModel flowViewModelRes = _mapper.Map<FlowWfViewModel>(flow);
                return flowViewModelRes;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating flow");
                throw;
            }
        }
    }
}
