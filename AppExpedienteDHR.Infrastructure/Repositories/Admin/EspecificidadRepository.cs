using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Admin
{
    public class EspecificidadRepository: Repository<Especificidad>, IEspecificidadRepository
    {
        private readonly ApplicationDbContext _context;
        public EspecificidadRepository(DbContext context) : base(context)
        {
            _context = context as ApplicationDbContext;
        }

        public async Task Update(Especificidad especificidad)
        {
            var especificidadToUpdate = await _context.Especificidades.FirstOrDefaultAsync(e => e.Id == especificidad.Id);
            if (especificidadToUpdate != null)
            {
                especificidadToUpdate.Codigo = especificidad.Codigo;
                especificidadToUpdate.Descripcion = especificidad.Descripcion;
                especificidadToUpdate.Normativa = especificidad.Normativa;
                await _context.SaveChangesAsync();
            }
        }
    }
}
