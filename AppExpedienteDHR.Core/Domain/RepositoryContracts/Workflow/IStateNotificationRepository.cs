using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IStateNotificationRepository :IRepository<StateNotificationWf>
    {
        Task Update(StateNotificationWf stateNotification);

    }
}
