using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IActionGroupWfRepository: IRepository<ActionGroupWf>
    {
        Task Update(ActionGroupWf actionGroup);

    }
}
