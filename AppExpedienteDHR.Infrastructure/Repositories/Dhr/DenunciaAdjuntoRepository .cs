using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Dhr
{
    public class DenunciaAdjuntoRepository : Repository<DenunciaAdjunto>, IDenunciaAdjuntoRepository
    {
        private readonly ApplicationDbContext _context;

        public DenunciaAdjuntoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DenunciaAdjunto>> ObtenerPorDenunciaId(int denunciaId)
        {
            return await _context.DenunciaAdjuntos
                .Where(da => da.DenunciaId == denunciaId)
                .Include(da => da.Adjunto)
                .ToListAsync();
        }
    }
}
