using AppExpedienteDHR.Core.ViewModels.Dhr;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ServiceContracts.Dhr
{
    public interface IDenunciaService
    {
        Task<DenunciaViewModel> CreateDenuncia(DenunciaViewModel viewModel);
        Task UpdateDenuncia(DenunciaViewModel viewModel);
        Task DeleteDenuncia(int id);
        Task<DenunciaViewModel> GetDenuncia(int id);
        Task AgregarAnexoDenuncia(int id, IFormFile file);
        Task<(List<DenunciaListadoViewModel> items, int totalItems)> GetDenunciasPaginadas(
             int pageIndex, int pageSize, string searchValue, string sortColumn, string sortDirection);

    }
}
