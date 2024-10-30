
using AppExpedienteDHR.Core.ViewModels.Admin;

namespace AppExpedienteDHR.Core.ServiceContracts.Admin
{
    public interface IDerechoTipologiaService
    {
        Task<IEnumerable<DerechoViewModel>> GetDerechoTipologia();
        Task<DerechoViewModel> GetDerechoTipologiaById(int id);
        Task<DerechoViewModel> GetDerechoTipologiaByCode(string id);
        Task<DerechoViewModel> InsertDerechoTipologia(DerechoViewModel derechoTipologia);
        Task<DerechoViewModel> UpdateDerechoTipologia(DerechoViewModel derechoTipologia);
        Task<bool> DeleteDerechoTipologia(int id);
    }
}
