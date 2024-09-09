using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.ServiceContracts.General
{
    public interface IEstadoCivilService
    {
        Task<IEnumerable<EstadoCivil>> GetAllEstadoCivil();
        Task<EstadoCivil> GetEstadoCivil(int id);
    }
}
