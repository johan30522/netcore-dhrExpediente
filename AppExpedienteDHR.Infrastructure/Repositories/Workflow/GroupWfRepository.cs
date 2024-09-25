using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class GroupWfRepository: Repository<GroupWf>, IGroupWfRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(GroupWf group)
        {
            GroupWf groupToUpdate = await _context.GroupWfs.FirstOrDefaultAsync(c => c.Id == group.Id);
            if (groupToUpdate != null)
            {

                groupToUpdate.Name = group.Name;
                groupToUpdate.Order = group.Order;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GroupWf>> GetGroupsByUserId(string userId)
        {
            return await _context.GroupWfs
                .Where(g => g.GroupUsers.Any(gu => gu.UserId == userId))
                .ToListAsync();
        }
    }

}
