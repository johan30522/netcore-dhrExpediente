using AppExpedienteDHR.Core.Domain.Entities.Dhr;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr
{
    public interface IPersonaAfectadaRepository : IRepository<PersonaAfectada>
    {
        Task Update(PersonaAfectada personaAfectada);
    }
}
