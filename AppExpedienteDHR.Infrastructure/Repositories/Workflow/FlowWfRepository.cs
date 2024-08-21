using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class FlowWfRepository: Repository<FlowWf>, IFlowWfRepository
    {
        private readonly ApplicationDbContext _context;

        public FlowWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(FlowWf flow)
        {
            FlowWf flowToUpdate = await _context.FlowWfs.FirstOrDefaultAsync(c => c.Id == flow.Id);
            if (flowToUpdate != null)
            {
                flowToUpdate.Name = flow.Name;
                flowToUpdate.Order = flow.Order;
                await _context.SaveChangesAsync();
            }
        }
    }

}
