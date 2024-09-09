using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using Serilog;

namespace AppExpedienteDHR.Core.Services.General
{
    public class TipoIdentificacionService: ITipoIdentificacionService
    {
        private readonly IContainerWork _containerWork;
        private readonly ILogger _logger;

        public TipoIdentificacionService(ILogger logger, IContainerWork containerWork)
        {
            _logger = logger;
            _containerWork = containerWork;
        }


        public async Task<IEnumerable<TipoIdentificacion>> GetAllTipoIdentificaciones()
        {
            try
            {
                IEnumerable<TipoIdentificacion> tipoIdentificaciones = await _containerWork.TipoIdentificacion.GetAll();
                return tipoIdentificaciones;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en TipoIdentificacionService.GetAllTipoIdentificacionesAsync");
                throw;
            }
        }

        public async Task<TipoIdentificacion> GetTipoIdentificacion(int id)
        {
            try
            {
                TipoIdentificacion tipoIdentificacion = await _containerWork.TipoIdentificacion.GetFirstOrDefault(t => t.TipoIdentificacionId == id);
                if (tipoIdentificacion == null)
                {
                    throw new Exception("TipoIdentificacion not found");
                }
                return tipoIdentificacion;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en TipoIdentificacionService.GetTipoIdentificacionAsync");
                throw;
            }
        }
    }
}
