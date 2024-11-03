using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Admin
{
    public class DescriptorRepository: Repository<Descriptor>, IDescriptorRepository
    {
        private readonly ApplicationDbContext _context;
        public DescriptorRepository(DbContext context) : base(context)
        {
            _context = context as ApplicationDbContext;
        }

        public async Task Update(Descriptor descriptor)
        {
            var descriptorToUpdate = await _context.Descriptores.FirstOrDefaultAsync(d => d.Id == descriptor.Id);
            if (descriptorToUpdate != null)
            {
                descriptorToUpdate.Codigo = descriptor.Codigo;
                descriptorToUpdate.Nombre = descriptor.Nombre;

                await _context.SaveChangesAsync();
            }
        }
    }

}
