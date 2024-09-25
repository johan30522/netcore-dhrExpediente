using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IActionWfRepository: IRepository<ActionWf>
    {
        Task Update(ActionWf action);
        Task<IEnumerable<ActionWf>> GetActionForStateAnGroups(int stateId, List<int> groupIds);
    }
}
