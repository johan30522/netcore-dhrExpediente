using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Infrastructure.Data;

namespace AppExpedienteDHR.Infrastructure.Repositories.General
{
    public class TipoIdentificacionRepository: Repository<TipoIdentificacion>, ITipoIdentificacionRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoIdentificacionRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Update(TipoIdentificacion tipoIdentificacion)
        {
            var currentTipoIdentificacion = await _context.TiposIdentificacion.FindAsync(tipoIdentificacion.TipoIdentificacionId);
            currentTipoIdentificacion.Descripcion = tipoIdentificacion.Descripcion;
            await _context.SaveChangesAsync();
        }
    }
}
