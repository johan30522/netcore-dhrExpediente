using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Dhr
{
    public class DenunciaRepository : Repository<Denuncia>, IDenunciaRepository
    {
        private readonly ApplicationDbContext _context;

        public DenunciaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Denuncia denuncia)
        {
            Denuncia denunciaToUpdate = await _context.Denuncias.FirstOrDefaultAsync(d => d.Id == denuncia.Id);
            if (denunciaToUpdate != null)
            {
                denunciaToUpdate.DetalleDenuncia = denuncia.DetalleDenuncia;
                denunciaToUpdate.Petitoria = denuncia.Petitoria;
                denunciaToUpdate.AceptaTerminos = denuncia.AceptaTerminos;
                await _context.SaveChangesAsync();
            }
        }

    }
}
