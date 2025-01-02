using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using Serilog;


namespace AppExpedienteDHR.Core.Services.WorkFlow
{
    public class RequestWfService: IRequestWfService
    {
        private readonly IContainerWork _unitOfWork;
        private readonly ILogger _logger;




        public RequestWfService(IContainerWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        /// <summary>
        /// Permite obtener la solicitud completa con todos los datos asociados
        /// De momento solo se obtiene la Denuncia y el Expediente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>

        public async Task<object> GetCompleteRequest(int id, string requestType)
        {

            try
            {
                switch (requestType)
                {
                    case "Denuncia":
                        var denuncia = await _unitOfWork.Denuncia.GetFirstOrDefault(x => x.Id == id, 
                                    includeProperties: "Denunciante,PersonaAfectada,DenunciaAdjuntos.Adjunto");
                        return denuncia;
                    case "Expediente":
                        var expediente = await _unitOfWork.Expediente.GetFirstOrDefault(e => e.Id == id,
                                    includeProperties: "Denunciante,PersonaAfectada,Derecho,Evento,Especificidad,ExpedienteAdjuntos.Adjunto");
                        return expediente;
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la solicitud completa");
                return null;
            }



        }



    }
}
