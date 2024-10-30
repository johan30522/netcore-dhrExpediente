using AppExpedienteDHR.Core.Domain.Entities.Admin;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin
{
    public interface IDerechoRepository: IRepository<Derecho>
    {
        Task Update(Derecho derecho);
    }
}
