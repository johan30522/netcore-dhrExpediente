

using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Admin
{
    public class DerechoRepository : Repository<Derecho>, IDerechoRepository

    {
        private readonly ApplicationDbContext _context;

        public DerechoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Derecho derecho)
        {
            var derechoToUpdate = await _context.Derechos.FirstOrDefaultAsync(d => d.Id == derecho.Id);
            if (derechoToUpdate != null)
            {
                derechoToUpdate.Codigo = derecho.Codigo;
                derechoToUpdate.Descripcion = derecho.Descripcion;


                await _context.SaveChangesAsync();
            }
        }
    }
}
