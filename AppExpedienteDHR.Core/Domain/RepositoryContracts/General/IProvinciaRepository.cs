using AppExpedienteDHR.Core.Domain.Entities.General;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.General
{
    public interface IProvinciaRepository: IRepository<Provincia>
    {
        Task Update(Provincia provincia);
    }
}
