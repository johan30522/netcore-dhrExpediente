using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using Serilog;

namespace AppExpedienteDHR.Core.Services.General
{
    public class EscolaridadService : IEscolaridadService
    {
        private readonly IContainerWork _containerWork;
        private readonly ILogger _logger;

        public EscolaridadService(ILogger logger, IContainerWork containerWork)
        {
            _logger = logger;
            _containerWork = containerWork;
        }

        public async Task<IEnumerable<Escolaridad>> GetAllEscolaridades()
        {
            try
            {
                IEnumerable<Escolaridad> escolaridades = await _containerWork.Escolaridad.GetAll();
                return escolaridades;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetAllEscolaridadesAsync");
                throw;
            }
        }

        public async Task<Escolaridad> GetEscolaridad(int id)
        {
            try
            {
                Escolaridad escolaridad = await _containerWork.Escolaridad.GetFirstOrDefault(e => e.EscolaridadId == id);
                if (escolaridad == null)
                {
                    throw new Exception("Escolaridad not found");
                }
                return escolaridad;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetEscolaridadAsync");
                throw;
            }
        }
    }
}
