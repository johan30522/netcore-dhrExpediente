using AppExpedienteDHR.Core.Domain.Entities.Dhr;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr
{
    public interface IDenunciaRepository : IRepository<Denuncia>
    {
        Task Update(Denuncia denuncia);
    }
}
