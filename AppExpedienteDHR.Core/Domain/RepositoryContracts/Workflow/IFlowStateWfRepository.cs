using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IFlowStateWfRepository: IRepository<FlowStateWf>
    {
        Task Update(FlowStateWf flowState);
    }
}
