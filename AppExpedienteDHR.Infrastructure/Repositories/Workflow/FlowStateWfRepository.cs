using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class FlowStateWfRepository: Repository<FlowStateWf>, IFlowStateWfRepository
    {
        private readonly ApplicationDbContext _context;

        public FlowStateWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(FlowStateWf flowState)
        {
            FlowStateWf flowStateToUpdate = await _context.FlowStateWfs.FirstOrDefaultAsync(c => c.Id == flowState.Id);
            if (flowStateToUpdate != null)
            {
                flowStateToUpdate.FlowId = flowState.FlowId;
                flowStateToUpdate.StateId = flowState.StateId;
                flowStateToUpdate.FlowId = flowState.FlowId;
                await _context.SaveChangesAsync();
            }
        }
    }
    
}
