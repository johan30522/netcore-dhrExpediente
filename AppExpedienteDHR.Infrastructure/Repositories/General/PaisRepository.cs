using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Infrastructure.Data;

namespace AppExpedienteDHR.Infrastructure.Repositories.General
{
    public class PaisRepository: Repository<Pais>, IPaisRepository
    {
        private readonly ApplicationDbContext _context;

        public PaisRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Update(Pais pais)
        {
            var currentPais = await _context.Paises.FindAsync(pais.CodigoNumerico);
            currentPais.DenominacionPais = pais.DenominacionPais;
            await _context.SaveChangesAsync();
        }
    }
}
