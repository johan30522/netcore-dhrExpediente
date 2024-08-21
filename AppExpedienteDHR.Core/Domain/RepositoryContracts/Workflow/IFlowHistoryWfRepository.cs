using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IFlowHistoryWfRepository: IRepository<FlowHistoryWf>
    {
        Task Update(FlowHistoryWf flowHistory);
    }
}
