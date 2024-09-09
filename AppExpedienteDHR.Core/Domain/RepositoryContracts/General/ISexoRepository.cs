using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.General
{
    public interface ISexoRepository : IRepository<Sexo>
    {
        Task Update(Sexo sexo);
    }
}
