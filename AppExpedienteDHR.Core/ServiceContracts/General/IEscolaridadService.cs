using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.ServiceContracts.General
{
    public interface IEscolaridadService
    {
        Task<IEnumerable<Escolaridad>> GetAllEscolaridades();
        Task<Escolaridad> GetEscolaridad(int id);
    }
}
