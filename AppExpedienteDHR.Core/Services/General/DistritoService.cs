using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using Serilog;

namespace AppExpedienteDHR.Core.Services.General
{
    public class DistritoService: IDistritoService
    {
        private readonly IContainerWork _containerWork;
        private readonly ILogger _logger;

        public DistritoService(ILogger logger, IContainerWork containerWork)
        {
            _logger = logger;
            _containerWork = containerWork;
        }
        

        public async Task<IEnumerable<Distrito>> GetAllDistritos( int idCanton)
        {
            try
            {
                IEnumerable<Distrito> distritos = await _containerWork.Distrito.GetAll(d => d.CodigoCanton == idCanton);
                return distritos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en DistritoService.GetAllDistritosAsync");
                throw;
            }
        }
       

        public async Task<Distrito> GetDistrito(int id)
        {
            try
            {
                return await _containerWork.Distrito.GetFirstOrDefault(d => d.CodigoDistrito == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en DistritoService.GetDistritoAsync");
                throw;
            }
        }
    }
}
