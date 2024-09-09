using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.ServiceContracts.General
{
    public interface ISexoService
    {
        Task<IEnumerable<Sexo>> GetAllSexos();
        Task<Sexo> GetSexo(int id);
    }
}
