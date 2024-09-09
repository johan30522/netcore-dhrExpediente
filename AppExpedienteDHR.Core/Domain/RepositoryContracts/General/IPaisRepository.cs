using AppExpedienteDHR.Core.Domain.Entities.General;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.General
{
    public interface IPaisRepository: IRepository<Pais>
    {
        Task Update(Pais pais);
    }
}
