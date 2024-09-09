using AppExpedienteDHR.Core.Domain.Entities.General;

namespace AppExpedienteDHR.Core.ServiceContracts.General
{
    public interface IPadronService
    {
        Task<IEnumerable<Padron>> GetAllCiudadanos();

        Task<Padron> GetCiudadano(string cedula);

    }
}
