using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.ServiceContracts.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using Serilog;

namespace AppExpedienteDHR.Core.Services.General
{
    public class PadronService : IPadronService
    {

        private readonly IContainerWork _containerWork;
        private readonly ILogger _logger;

        public PadronService(ILogger logger, IContainerWork containerWork)
        {
            _logger = logger;
            _containerWork = containerWork;
        }


        public async Task<IEnumerable<Padron>> GetAllCiudadanos()
        {
            try
            {
                IEnumerable<Padron> ciudadanos = await _containerWork.Padron.GetAll();
                return ciudadanos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetAllCiudadanos");
                throw;
            }
        }

        public async Task<Padron> GetCiudadano(string cedula)
        {
            try
            {
                Padron ciudadano = await _containerWork.Padron.GetFirstOrDefault(c => c.Cedula == cedula);
                if (ciudadano == null)
                {
                    return null;
                }
                return ciudadano;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetCiudadano");
                throw;
            }
        }
    }
}
