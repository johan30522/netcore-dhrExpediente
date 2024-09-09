using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using Serilog;

namespace AppExpedienteDHR.Core.Services.General
{
    public class SexoService: ISexoService
    {
        private readonly IContainerWork _containerWork;
        private readonly ILogger _logger;

        public SexoService(ILogger logger, IContainerWork containerWork)
        {
            _logger = logger;
            _containerWork = containerWork;
        }

        public async Task<IEnumerable<Sexo>> GetAllSexos()
        {
            try
            {
                IEnumerable<Sexo> sexos = await _containerWork.Sexo.GetAll();
                return sexos;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en SexoService.GetAllSexosAsync");
                throw;
            }
        }

        public async Task<Sexo> GetSexo(int id)
        {
            try
            {
                Sexo sexo = await _containerWork.Sexo.GetFirstOrDefault(s => s.SexoId == id);
                if (sexo == null)
                {
                    throw new Exception("Sexo not found");
                }
                return sexo;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en SexoService.GetSexoAsync");
                throw;
            }
        }
    }
}
