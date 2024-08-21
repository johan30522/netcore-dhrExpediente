using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IStateWfRepository : IRepository<StateWf>
    {
        Task Update(StateWf state);
    }
}
