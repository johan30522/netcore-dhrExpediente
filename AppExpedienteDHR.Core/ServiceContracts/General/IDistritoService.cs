using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.ServiceContracts.General
{
    public interface IDistritoService
    {
        Task<IEnumerable<Distrito>> GetAllDistritos( int idCanton);
        Task<Distrito> GetDistrito(int id);
    }
}
