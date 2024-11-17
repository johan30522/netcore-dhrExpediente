using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;


namespace AppExpedienteDHR.Core.ServiceContracts.Dhr
{
    public interface IDenuncianteService
    {
        Task<DenuncianteViewModel> CreateDenunciante(DenuncianteViewModel viewModel);
        Task UpdateDenunciante(DenuncianteViewModel viewModel);
        Task<DenuncianteViewModel> GetDenunciante(int id);
        Task<Denunciante> CreateOrUpdate(DenuncianteViewModel viewModel);



    }
}
