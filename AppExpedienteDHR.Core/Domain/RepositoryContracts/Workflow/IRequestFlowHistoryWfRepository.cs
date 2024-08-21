using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IRequestFlowHistoryWfRepository: IRepository<RequestFlowHistoryWf>
    {
        Task Update(RequestFlowHistoryWf requestFlowHistory);
    }
}
