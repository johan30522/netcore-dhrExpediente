using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IFlowWfRepository : IRepository<FlowWf>
    {
        Task Update(FlowWf flow);
    }
}
