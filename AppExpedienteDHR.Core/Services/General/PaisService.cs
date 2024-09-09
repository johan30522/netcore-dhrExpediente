using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using Serilog;

namespace AppExpedienteDHR.Core.Services.General
{
    public class PaisService: IPaisService
    {
        private readonly IContainerWork _containerWork;
        private readonly ILogger _logger;

        public PaisService(ILogger logger, IContainerWork containerWork)
        {
            _logger = logger;
            _containerWork = containerWork;
        }

        public async Task<IEnumerable<Pais>> GetAllPaises()
        {
            try
            {
                IEnumerable<Pais> paises = await _containerWork.Pais.GetAll();
                return paises;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetAllPaisesAsync");
                throw;
            }
        }

        public async Task<Pais> GetPais(int id)
        {
            try
            {
                Pais pais = await _containerWork.Pais.GetFirstOrDefault(p => p.CodigoNumerico == id);
                if (pais == null)
                {
                    throw new Exception("Pais not found");
                }
                return pais;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetPaisAsync");
                throw;
            }
        }
    }
}
