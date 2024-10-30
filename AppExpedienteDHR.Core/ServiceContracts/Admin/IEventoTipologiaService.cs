
using AppExpedienteDHR.Core.ViewModels.Admin;

namespace AppExpedienteDHR.Core.ServiceContracts.Admin
{
    public interface IEventoTipologiaService
    {
        Task<IEnumerable<EventoViewModel>> GetEventos(int derechoId);
        Task<EventoViewModel> GetEventoById(int id);
        Task<EventoViewModel> InsertEvento(EventoViewModel evento);
        Task<EventoViewModel> UpdateEvento(EventoViewModel evento);
        Task<EventoViewModel> GetEventoByCode(string code);
        Task<bool> DeleteEvento(int id);
    }
}
