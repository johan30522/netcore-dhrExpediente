using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Dhr
{
    public class ExpedienteAdjuntoRepository : Repository<ExpedienteAdjunto>, IExpedienteAdjuntoRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpedienteAdjuntoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExpedienteAdjunto>> ObtenerPorExpedienteId(int expedienteId)
        {
            return await _context.ExpedienteAdjuntos
                .Where(da => da.ExpedienteId == expedienteId)
                .Include(da => da.Adjunto)
                .ToListAsync();
        }
    }
}
