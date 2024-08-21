using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IGroupUserWfRepository: IRepository<GroupUserWf>
    {
        Task Update(GroupUserWf groupUser);
    }
}
