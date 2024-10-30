

using AppExpedienteDHR.Core.ViewModels.Admin;

namespace AppExpedienteDHR.Core.ServiceContracts.Admin
{
    public interface IEspecificidadTipologiaService
    {
        Task<IEnumerable<EspecificidadViewModel>> GetEspecificidades( int eventoId);
        Task<EspecificidadViewModel> GetEspecificidadById(int id);
        Task<EspecificidadViewModel> GetEspecificidadByCode(string code);
        Task<EspecificidadViewModel> InsertEspecificidad(EspecificidadViewModel especificidad);
        Task<EspecificidadViewModel> UpdateEspecificidad(EspecificidadViewModel especificidad);
        Task<bool> DeleteEspecificidad(int id);
    }
}
