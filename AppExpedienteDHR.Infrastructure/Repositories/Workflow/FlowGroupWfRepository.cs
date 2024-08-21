using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class FlowGroupWfRepository: Repository<FlowGroupWf>, IFlowGroupWfRepository
    {
        private readonly ApplicationDbContext _context;

        public FlowGroupWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(FlowGroupWf flowGroup)
        {
            FlowGroupWf flowGroupToUpdate = await _context.FlowGroupWfs.FirstOrDefaultAsync(c => c.Id == flowGroup.Id);
            if (flowGroupToUpdate != null)
            {
                flowGroupToUpdate.FlowId = flowGroup.FlowId;
                flowGroupToUpdate.GroupId = flowGroup.GroupId;
                await _context.SaveChangesAsync();
            }
        }
    }

}
