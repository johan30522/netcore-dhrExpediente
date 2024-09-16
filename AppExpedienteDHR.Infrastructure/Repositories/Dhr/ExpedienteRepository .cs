using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace AppExpedienteDHR.Infrastructure.Repositories.Dhr
{
    public class ExpedienteRepository : Repository<Expediente>, IExpedienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpedienteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Expediente expediente)
        {
            Expediente expedienteToUpdate = await _context.Expedientes.FirstOrDefaultAsync(e => e.Id == expediente.Id);
            if (expedienteToUpdate != null)
            {
                expedienteToUpdate.EstadoActual = expediente.EstadoActual;
                await _context.SaveChangesAsync();
            }
        }
    }
}
