using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Infrastructure.Data;
using AppExpedienteDHR.Infrastructure.Repositories;


namespace BlogCore.Infrastructure.Repositories
{
    public class ContainerWork : IContainerWork
    {
        private readonly ApplicationDbContext _context;


        public ContainerWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);
           
           
        }
        public ICategoryRepository Category { get; private set; }

        public IUserRepository User { get; private set; }
      

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
