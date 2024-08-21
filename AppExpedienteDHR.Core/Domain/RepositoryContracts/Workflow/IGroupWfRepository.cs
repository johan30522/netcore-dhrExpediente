using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IGroupWfRepository: IRepository<GroupWf>
    {
        Task Update(GroupWf group);
    }
}
