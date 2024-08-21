using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class ActionGroupWfRepository: Repository<ActionGroupWf>, IActionGroupWfRepository
    {
        private readonly ApplicationDbContext _context;

        public ActionGroupWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(ActionGroupWf actionGroup)
        {
            ActionGroupWf actionGroupToUpdate = await _context.ActionGroupWfs.FirstOrDefaultAsync(c => c.Id == actionGroup.Id);
            if (actionGroupToUpdate != null)
            {
                actionGroupToUpdate.ActionId = actionGroup.ActionId;
                actionGroupToUpdate.GroupId = actionGroup.GroupId;
                await _context.SaveChangesAsync();
            }
        }
    }
}
