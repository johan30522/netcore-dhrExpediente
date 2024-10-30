using AppExpedienteDHR.Core.Domain.Entities.Admin;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin
{
   public interface IEspecificidadRepository: IRepository<Especificidad>
    {
        Task Update(Especificidad especificidad);
    }
}
