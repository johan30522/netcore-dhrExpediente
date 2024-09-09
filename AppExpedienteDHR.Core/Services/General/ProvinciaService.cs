using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using Serilog;

namespace AppExpedienteDHR.Core.Services.General
{
    public class ProvinciaService : IProvinciaService
    {
        private readonly ILogger _logger;
        private readonly IContainerWork _containerWork;

        public ProvinciaService(ILogger logger, IContainerWork containerWork)
        {
            _logger = logger;
            _containerWork = containerWork;
        }

        public async Task<IEnumerable<Provincia>> GetAllProvincias()
        {
            try
            {
                IEnumerable<Provincia> provincias = await _containerWork.Provincia.GetAll();
                return provincias;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetAllProvinciasAsync");
                throw;
            }

        }

        public async Task<Provincia> GetProvincia(int id)
        {
            try
            {
                Provincia provincia = await _containerWork.Provincia.GetFirstOrDefault(p => p.Codigo == id);
                if (provincia == null)
                {
                    throw new Exception("Provincia not found");
                }
                return provincia;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetProvinciaAsync");
                throw;
            }
        }
    }
}
