using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using Serilog;

namespace AppExpedienteDHR.Core.Services.General
{
    public class CantonService : ICantonService
    {
        private readonly ILogger _logger;
        private readonly IContainerWork _containerWork;

        public CantonService(ILogger logger, IContainerWork containerWork)
        {
            _logger = logger;
            _containerWork = containerWork;
        }

        public async Task<IEnumerable<Canton>> GetAllCantones(int codigoProvincia)
        {
            try
            {
                IEnumerable<Canton> cantones = await _containerWork.Canton.GetAll(c => c.CodigoProvincia == codigoProvincia);
                return cantones;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetAllCantonesAsync");
                throw;
            }
        }
        public async Task<Canton> GetCanton(int id)
        {
            try
            {
                // include los distritos
                Canton canton = await _containerWork.Canton.GetFirstOrDefault(c => c.CodigoCanton == id, includeProperties: "Distritos");
                if (canton == null)
                {
                    throw new Exception("Canton not found");
                }
                return canton;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetCantonAsync");
                throw;
            }
        }
    }
}
