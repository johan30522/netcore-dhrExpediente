using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;


namespace AppExpedienteDHR.Core.ServiceContracts.Dhr
{
    public interface IPersonaAfectadaService
    {
        Task<PersonaAfectada> CreateOrUpdate(PersonaAfectadaViewModel viewModel);
    }
}
