

using AppExpedienteDHR.Core.Domain.Entities.Admin;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin
{
    public interface IEventoRepository: IRepository<Evento>
    {
        Task Update(Evento evento);
    }
}
