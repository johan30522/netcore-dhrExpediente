using AppExpedienteDHR.Core.Domain.Entities.Dhr;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr
{
    public interface IDenuncianteRepository : IRepository<Denunciante>
    {
        Task <Denunciante> Update(Denunciante denunciante);
    }
}
