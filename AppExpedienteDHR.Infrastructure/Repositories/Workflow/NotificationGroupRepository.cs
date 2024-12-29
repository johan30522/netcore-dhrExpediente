using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class NotificationGroupRepository : Repository<NotificationGroupWf>, INotificationGroupWfRepository
    {
        private readonly ApplicationDbContext _context;
        public NotificationGroupRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       
    }
}
