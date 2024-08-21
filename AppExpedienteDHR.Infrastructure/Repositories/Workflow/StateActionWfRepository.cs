using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class StateActionWfRepository: Repository<StateActionWf>, IStateActionWfRepository
    {
        private readonly ApplicationDbContext _context;

        public StateActionWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(StateActionWf stateAction)
        {
            StateActionWf stateActionToUpdate = await _context.StateActionWfs.FirstOrDefaultAsync(c => c.Id == stateAction.Id);
            if (stateActionToUpdate != null)
            {
                stateActionToUpdate.StateId = stateAction.StateId;
                stateActionToUpdate.ActionId = stateAction.ActionId;
                await _context.SaveChangesAsync();
            }
        }
    }
    
}
