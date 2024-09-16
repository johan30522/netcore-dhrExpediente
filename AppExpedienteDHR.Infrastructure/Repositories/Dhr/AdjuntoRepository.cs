using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Dhr
{
    public class AdjuntoRepository : Repository<Adjunto>, IAdjuntoRepository
    {
        private readonly ApplicationDbContext _context;

        public AdjuntoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Adjunto> ObtenerPorRuta(string ruta)
        {
            return await _context.Adjuntos.FirstOrDefaultAsync(a => a.Ruta == ruta);
        }
    }
}
