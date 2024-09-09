using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using Serilog;

namespace AppExpedienteDHR.Core.Services.General
{
    public class EstadoCivilService : IEstadoCivilService
    {
        private readonly IContainerWork _containerWork;
        private readonly ILogger _logger;

        public EstadoCivilService(ILogger logger, IContainerWork containerWork)
        {
            _logger = logger;
            _containerWork = containerWork;
        }


        public async Task<IEnumerable<EstadoCivil>> GetAllEstadoCivil()
        {
            try
            {
                IEnumerable<EstadoCivil> estadoCivil = await _containerWork.EstadoCivil.GetAll();
                return estadoCivil;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetAllEstadoCivilAsync");
                throw;
            }
        }

        public async Task<EstadoCivil> GetEstadoCivil(int id)
        {
            try
            {
                EstadoCivil estadoCivil = await _containerWork.EstadoCivil.GetFirstOrDefault(e => e.EstadoCivilId == id);
                if (estadoCivil == null)
                {
                    throw new Exception("EstadoCivil not found");
                }
                return estadoCivil;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en GetEstadoCivilAsync");
                throw;
            }
        }
    }
}
