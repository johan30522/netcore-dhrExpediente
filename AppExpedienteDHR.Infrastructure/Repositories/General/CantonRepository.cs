using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Infrastructure.Data;


namespace AppExpedienteDHR.Infrastructure.Repositories.General
{
    public class CantonRepository: Repository<Canton>, ICantonRepository
    {
        private readonly ApplicationDbContext _context;

        public CantonRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Update(Canton canton)
        {
            var currentCanton = await _context.Cantones.FindAsync(canton.CodigoCanton);
            currentCanton.NombreCanton = canton.NombreCanton;
            await _context.SaveChangesAsync();
        }
    }
}
