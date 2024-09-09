using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.ServiceContracts.General
{
    public interface ITipoIdentificacionService
    {
        Task<IEnumerable<TipoIdentificacion>> GetAllTipoIdentificaciones();
        Task<TipoIdentificacion> GetTipoIdentificacion(int id);
    }
}
