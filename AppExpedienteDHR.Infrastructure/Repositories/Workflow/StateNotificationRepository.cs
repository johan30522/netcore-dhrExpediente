using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class StateNotificationRepository: Repository<StateNotificationWf>, IStateNotificationRepository
    {
        private readonly ApplicationDbContext _context;
        public StateNotificationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(StateNotificationWf stateNotification)
        {
            StateNotificationWf stateNotificationToUpdate = await _context.StateNotificationsWfs.FirstOrDefaultAsync(c => c.Id == stateNotification.Id);
            if (stateNotificationToUpdate != null)
            {
                stateNotificationToUpdate.EmailTemplateId = stateNotification.EmailTemplateId;
                stateNotificationToUpdate.To = stateNotification.To;
                stateNotificationToUpdate.Cc = stateNotification.Cc;
                stateNotificationToUpdate.Bcc = stateNotification.Bcc;
                await _context.SaveChangesAsync();
            }
        }
    }

}
