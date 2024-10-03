using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.ViewModels.Dhr;


namespace AppExpedienteDHR.Core.ServiceContracts.Dhr
{
    public interface IExpedienteService
    {
        Task<ExpedienteViewModel> CreateExpediente(ExpedienteViewModel viewModel);
        Task UpdateExpediente(ExpedienteViewModel viewModel);
        Task<ExpedienteViewModel> GetExpediente(int id);
        Task<(List<ExpedienteItemListViewModel> items, int totalItems)> GetExpedientesPaginadas(
            int pageIndex, int pageSize, string searchValue, string sortColumn, string sortDirection);
     
    }
}
