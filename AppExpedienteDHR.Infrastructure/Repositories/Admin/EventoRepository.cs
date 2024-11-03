using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Admin
{
    public class EventoRepository: Repository<Evento>, IEventoRepository
    {
        private readonly ApplicationDbContext _context;
        public EventoRepository(DbContext context) : base(context)
        {
            _context = context as ApplicationDbContext;
        }

        public async Task Update(Evento evento)
        {
            var eventoToUpdate = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == evento.Id);
            if (eventoToUpdate != null)
            {
                eventoToUpdate.Codigo = evento.Codigo;
                eventoToUpdate.Descripcion = evento.Descripcion;
                eventoToUpdate.Normativa = evento.Normativa;
                eventoToUpdate.ODS = evento.ODS;

                await _context.SaveChangesAsync();
            }
        }
    }

}
