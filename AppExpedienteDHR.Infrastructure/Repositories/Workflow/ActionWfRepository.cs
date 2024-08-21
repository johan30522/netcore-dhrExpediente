using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class ActionWfRepository : Repository<ActionWf>, IActionWfRepository
    {
        private readonly ApplicationDbContext _context;

        public ActionWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(ActionWf action)
        {
            ActionWf actionToUpdate = await _context.ActionWfs.FirstOrDefaultAsync(c => c.Id == action.Id);
            if (actionToUpdate != null)
            {
                actionToUpdate.Name = action.Name;
           
                actionToUpdate.Order = action.Order;
                actionToUpdate.NextStateId = action.NextStateId;
                actionToUpdate.EvaluationType = action.EvaluationType;

                await _context.SaveChangesAsync();
            }
        }
    }
  
}
