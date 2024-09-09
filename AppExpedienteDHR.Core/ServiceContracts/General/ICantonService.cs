using AppExpedienteDHR.Core.Domain.Entities.General;

namespace AppExpedienteDHR.Core.ServiceContracts.General
{
    public interface ICantonService
    {
        Task<IEnumerable<Canton>> GetAllCantones(int codigoProvincia);
        Task<Canton> GetCanton(int id);

    }
}
