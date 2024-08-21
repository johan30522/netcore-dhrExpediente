using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IFlowGroupWfRepository: IRepository<FlowGroupWf>
    {
        Task Update(FlowGroupWf flowGroup);

    }
}
