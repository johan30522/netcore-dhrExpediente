using AppExpedienteDHR.Core.ViewModels.Dhr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ServiceContracts.Dhr
{
    public interface IExpedienteService
    {
        Task<ExpedienteViewModel> CreateExpediente(ExpedienteViewModel viewModel);
        Task UpdateExpediente(ExpedienteViewModel viewModel);
        Task<ExpedienteViewModel> GetExpediente(int id);
        Task<(List<ExpedienteListadoViewModel> items, int totalItems)> GetExpedientesPaginadas(
            int pageIndex, int pageSize, string searchValue, string sortColumn, string sortDirection);
    }
}
