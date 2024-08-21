using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class FlowHistoryWfRepository: Repository<FlowHistoryWf>, IFlowHistoryWfRepository
    {
        private readonly ApplicationDbContext _context;

        public FlowHistoryWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(FlowHistoryWf flowHistory)
        {
            FlowHistoryWf flowHistoryToUpdate = await _context.FlowHistoryWfs.FirstOrDefaultAsync(c => c.Id == flowHistory.Id);
            if (flowHistoryToUpdate != null)
            {
                flowHistoryToUpdate.FlowId = flowHistory.FlowId;
                flowHistoryToUpdate.PreviousStateId = flowHistory.PreviousStateId;
                flowHistoryToUpdate.NewStateId = flowHistory.NewStateId;
                flowHistoryToUpdate.ActionPerformedId = flowHistory.ActionPerformedId;
                flowHistoryToUpdate.PerformedByUserId = flowHistory.PerformedByUserId;
                flowHistoryToUpdate.Comments = flowHistory.Comments;
            }
        }
    }
}
