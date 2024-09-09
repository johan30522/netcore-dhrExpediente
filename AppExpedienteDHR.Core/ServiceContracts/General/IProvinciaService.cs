using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.ServiceContracts.General
{
    public interface IProvinciaService
    {
        Task<IEnumerable<Provincia>> GetAllProvincias();
        Task<Provincia> GetProvincia(int id);
    }
}
