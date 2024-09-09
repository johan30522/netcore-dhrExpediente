using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Infrastructure.Data;

namespace AppExpedienteDHR.Infrastructure.Repositories.General
{
    public class EscolaridadRepository: Repository<Escolaridad>, IEscolaridadRepository
    {
        private readonly ApplicationDbContext _context;

        public EscolaridadRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Update(Escolaridad escolaridad)
        {
            var currentEscolaridad = await _context.Escolaridades.FindAsync(escolaridad.EscolaridadId);
            currentEscolaridad.Descripcion = escolaridad.Descripcion;
            await _context.SaveChangesAsync();
        }
    }
}
