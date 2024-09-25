using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IFlowRequestHeaderWfRepository: IRepository<FlowRequestHeaderWf>
    {
        Task Update(FlowRequestHeaderWf requestFlowHistory);
    }
}
