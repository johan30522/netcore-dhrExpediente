
using AppExpedienteDHR.Core.ViewModels.Admin;


namespace AppExpedienteDHR.Core.ServiceContracts.Admin
{
    public interface IDescriptorTipologiaService
    {
        Task<IEnumerable<DescriptorViewModel>> GetDescriptors(int eventoId);
        Task<DescriptorViewModel> GetDescriptorById(int id);
        Task<DescriptorViewModel> GetDescriptorByCode(string code);
        Task<DescriptorViewModel> InsertDescriptor(DescriptorViewModel descriptor);
        Task<DescriptorViewModel> UpdateDescriptor(DescriptorViewModel descriptor);
        Task<bool> DeleteDescriptor(int id);
    }
}
