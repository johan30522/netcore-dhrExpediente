using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IStateActionWfRepository: IRepository<StateActionWf>
    {
        Task Update(StateActionWf stateAction);
    }
}
