using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Infrastructure.Data;
namespace AppExpedienteDHR.Infrastructure.Repositories.General
{
    public class DistritoRepository: Repository<Distrito>, IDistritoRepository
    {
        private readonly ApplicationDbContext _context;

        public DistritoRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task Update(Distrito distrito)
        {
            var currentDistrito = await _context.Distritos.FindAsync(distrito.CodigoDistrito);
            currentDistrito.NombreDistrito = distrito.NombreDistrito;
            await _context.SaveChangesAsync();
           
        }

    }
}
