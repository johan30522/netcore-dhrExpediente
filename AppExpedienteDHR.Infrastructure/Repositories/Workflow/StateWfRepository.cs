using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class StateWfRepository: Repository<StateWf>, IStateWfRepository
    {
        private readonly ApplicationDbContext _context;

        public StateWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(StateWf state)
        {
            StateWf stateToUpdate = await _context.StateWfs.FirstOrDefaultAsync(c => c.Id == state.Id);
            if (stateToUpdate != null)
            {
                stateToUpdate.Name = state.Name;
                stateToUpdate.Order = state.Order;
                stateToUpdate.IsInitialState = state.IsInitialState;
                stateToUpdate.IsFinalState = state.IsFinalState;
                stateToUpdate.IsNotificationActive = state.IsNotificationActive;
                await _context.SaveChangesAsync();
            }
        }
    }

}
