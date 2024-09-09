using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.General
{
    public interface ICantonRepository :IRepository<Canton>
    {
        Task Update(Canton canton);
    }
}
