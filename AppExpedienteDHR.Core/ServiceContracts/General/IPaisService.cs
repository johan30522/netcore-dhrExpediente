using AppExpedienteDHR.Core.Domain.Entities.General;

namespace AppExpedienteDHR.Core.ServiceContracts.General
{
    public interface IPaisService
    {
        Task<IEnumerable<Pais>> GetAllPaises();
        Task<Pais> GetPais(int id);
    }
}
