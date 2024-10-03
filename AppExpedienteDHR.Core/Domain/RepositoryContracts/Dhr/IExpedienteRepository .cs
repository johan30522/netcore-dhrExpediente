using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr
{
    public interface IExpedienteRepository : IRepository<Expediente>
    {
        Task Update(Expediente expediente);
        Task<(List<ExpedienteItemListViewModel> items, int totalItems)> GetExpedientesWithFlowPaginated(
            int pageIndex = 0,
            int pageSize = 10,
            string searchValue = "",
            string sortColumn = "FechaCreacion",
            string sortDirection = "asc"
        );
    }
}
