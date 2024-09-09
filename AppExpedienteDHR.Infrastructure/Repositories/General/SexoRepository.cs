using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Infrastructure.Data;

namespace AppExpedienteDHR.Infrastructure.Repositories.General
{
    public class SexoRepository: Repository<Sexo>, ISexoRepository
    {
        private readonly ApplicationDbContext _context;

        public SexoRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Update(Sexo sexo)
        {
            var currentSexo = await _context.Sexos.FindAsync(sexo.SexoId);
            currentSexo.Descripcion = sexo.Descripcion;
            await _context.SaveChangesAsync();
        }
    }

}
