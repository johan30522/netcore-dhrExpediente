using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class RequestFlowHistoryWfRepository: Repository<RequestFlowHistoryWf>, IRequestFlowHistoryWfRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestFlowHistoryWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(RequestFlowHistoryWf requestFlowHistory)
        {
            RequestFlowHistoryWf requestFlowHistoryToUpdate = await _context.RequestFlowHistoryWfs.FirstOrDefaultAsync(c => c.Id == requestFlowHistory.Id);
            if (requestFlowHistoryToUpdate != null)
            {
                requestFlowHistoryToUpdate.RequestId = requestFlowHistory.RequestId;
                requestFlowHistoryToUpdate.FlowHistoryId = requestFlowHistory.FlowHistoryId;
              
                await _context.SaveChangesAsync();
            }
        }
    }
    
}
