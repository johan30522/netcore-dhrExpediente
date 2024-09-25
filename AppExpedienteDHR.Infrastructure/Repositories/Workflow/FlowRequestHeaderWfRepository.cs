using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class FlowRequestHeaderWfRepository: Repository<FlowRequestHeaderWf>, IFlowRequestHeaderWfRepository
    {
        private readonly ApplicationDbContext _context;

        public FlowRequestHeaderWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(FlowRequestHeaderWf requestFlowHistory)
        {
            FlowRequestHeaderWf requestFlowHistoryToUpdate = await _context.FlowRequestHeaderWfs.FirstOrDefaultAsync(c => c.Id == requestFlowHistory.Id);
            if (requestFlowHistoryToUpdate != null)
            {
                requestFlowHistoryToUpdate.RequestType= requestFlowHistory.RequestType;
                requestFlowHistoryToUpdate.FlowId = requestFlowHistory.FlowId;
                requestFlowHistoryToUpdate.CurrentStateId= requestFlowHistory.CurrentStateId;
                requestFlowHistoryToUpdate.CompletedDate= requestFlowHistory.CompletedDate;
                requestFlowHistoryToUpdate.IsCompleted= requestFlowHistory.IsCompleted;

                await _context.SaveChangesAsync();
            }
        }
    }
    
}
