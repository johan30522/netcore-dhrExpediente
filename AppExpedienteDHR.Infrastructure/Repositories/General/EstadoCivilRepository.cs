using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Infrastructure.Data;

namespace AppExpedienteDHR.Infrastructure.Repositories.General
{
    public class EstadoCivilRepository: Repository<EstadoCivil>, IEstadoCivilRepository
    {
        private readonly ApplicationDbContext _context;

        public EstadoCivilRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Update(EstadoCivil estadoCivil)
        {
            var currentEstadoCivil = await _context.EstadosCiviles.FindAsync(estadoCivil.EstadoCivilId);
            currentEstadoCivil.Descripcion = estadoCivil.Descripcion;
            await _context.SaveChangesAsync();
        }
    }
}
