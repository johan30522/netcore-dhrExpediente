using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace AppExpedienteDHR.Infrastructure.Repositories.Dhr
{
    public class PersonaAfectadaRepository : Repository<PersonaAfectada>, IPersonaAfectadaRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonaAfectadaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(PersonaAfectada personaAfectada)
        {
            PersonaAfectada personaToUpdate = await _context.PersonasAfectadas.FirstOrDefaultAsync(pa => pa.Id == personaAfectada.Id);
            if (personaToUpdate != null)
            {
                personaToUpdate.Nombre = personaAfectada.Nombre;
                personaToUpdate.PrimerApellido = personaAfectada.PrimerApellido;
                await _context.SaveChangesAsync();
            }
        }
    }
}
