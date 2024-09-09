using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Infrastructure.Data;

namespace AppExpedienteDHR.Infrastructure.Repositories.General
{
    public class PadronRepository: Repository<Padron>, IPadronRepository
    {
        private readonly CatalogDbContext _context;

        public PadronRepository(CatalogDbContext context): base(context)
        {
            _context = context;
        }
       
    }

}
