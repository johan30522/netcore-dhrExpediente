using AppExpedienteDHR.Core.ViewModels.Dhr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ServiceContracts.Dhr
{
    public interface IDenuncianteService
    {
        Task<DenuncianteViewModel> CreateDenunciante(DenuncianteViewModel viewModel);
        Task UpdateDenunciante(DenuncianteViewModel viewModel);
        Task<DenuncianteViewModel> GetDenunciante(int id);
    }
}
