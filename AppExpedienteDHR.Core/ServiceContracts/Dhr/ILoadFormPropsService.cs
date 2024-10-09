using AppExpedienteDHR.Core.ViewModels.Dhr;


namespace AppExpedienteDHR.Core.ServiceContracts.Dhr
{
    public interface ILoadFormPropsService
    {
        Task<DenunciaViewModel> LoadCatalogsForDenuncia(DenunciaViewModel model);
        Task<ExpedienteViewModel> LoadCatalogsForExpediente(ExpedienteViewModel model);
    }
}
