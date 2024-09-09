using AppExpedienteDHR.Core.Domain.Entities.General;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.General
{
    public interface IDistritoRepository: IRepository<Distrito>
    {
        Task Update(Distrito distrito);
    }
}
