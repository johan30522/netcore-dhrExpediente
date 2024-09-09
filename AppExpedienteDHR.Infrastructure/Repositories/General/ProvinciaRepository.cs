using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Infrastructure.Data;

namespace AppExpedienteDHR.Infrastructure.Repositories.General
{
    public class ProvinciaRepository: Repository<Provincia>, IProvinciaRepository
    {
        private readonly ApplicationDbContext _context;

        public ProvinciaRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Update(Provincia provincia)
        {
            var currentProvincia = await _context.Provincias.FindAsync(provincia.Codigo);
            currentProvincia.NombreProvincia = provincia.NombreProvincia;
            await _context.SaveChangesAsync();
        }
    }
}
