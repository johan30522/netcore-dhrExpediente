using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class GroupUserWfRepository : Repository<GroupUserWf>, IGroupUserWfRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupUserWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(GroupUserWf groupUser)
        {
            GroupUserWf groupUserToUpdate = await _context.GroupUserWfs.FirstOrDefaultAsync(c => c.Id == groupUser.Id);
            if (groupUserToUpdate != null)
            {
                groupUserToUpdate.GroupId = groupUser.GroupId;
                groupUserToUpdate.UserId = groupUser.UserId;
                await _context.SaveChangesAsync();
            }
        }
    }

}
