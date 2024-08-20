using AppExpedienteDHR.Core.Domain.IdentityEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;


        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task LockUser(string userId)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync( user => user.Id == userId);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnLockUser(string userId)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync( user => user.Id == userId);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now;
                await _context.SaveChangesAsync();
            }


        }
    }
}
