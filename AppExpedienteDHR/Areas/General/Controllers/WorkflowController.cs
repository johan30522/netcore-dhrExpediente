using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AppExpedienteDHR.Core.ServiceContracts.Utils;


namespace AppExpedienteDHR.Areas.General.Controllers
{
    [Authorize]
    [Area("General")]
    [Route("General/[controller]/[action]")]
    public class WorkflowController : Controller
    {
        private readonly IWorkflowService _workflowService;
        private readonly Serilog.ILogger _logger;


        public WorkflowController(IWorkflowService workflowService, Serilog.ILogger logger)
        {
            _workflowService = workflowService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessAction(int requestId, int actionId, string comments)
        {
            try
            {
                // Procesar la acción en el flujo
                var flowHeader= await _workflowService.ProcessAction<AppExpedienteDHR.Core.Domain.Entities.Dhr.Expediente>(requestId, actionId, comments);  // Puedes pasar otros tipos de solicitudes como Expediente
                // si todo sale bien, debuelvo ok y el id de la solicitud y el tipo de solicitud
                return Ok(new { flowHeader.RequestId, flowHeader.RequestType });

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error procesando la acción en el flujo para la solicitud {RequestId}", requestId);
                return BadRequest("Error procesando la acción.");
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetAvailableActions([FromQuery] int flowId, [FromQuery] int currentStateId)
        {

            try
            {
                // Obtener las acciones disponibles basadas en el flujo, estado actual y el usuario actual
                var actions = await _workflowService.GetAvailableActions(flowId, currentStateId);
                if (actions == null || !actions.Any())
                {
                    return NotFound("No hay acciones disponibles para este flujo y estado.");
                }

                //var actionsList = actions.Select(a => new { a.Id, a.Name });
                var actionsList = actions.Select(a => new { a.Id, a.Name }).ToList();

                // Devolver las acciones en formato JSON
                return Json(actions.Select(a => new { a.Id, a.Name }));
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "Error al obtener las acciones disponibles para el flujo {FlowId} y estado {CurrentStateId}", flowId, currentStateId);
                _logger.Error(ex, "Error al obtener las acciones disponibles.");
                return StatusCode(500, "Error al obtener las acciones disponibles.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFlowHistory(int requestFlowHeaderId)
        {
            try
            {
                var history = await _workflowService.GetFlowHistoryByRequestHeaderId(requestFlowHeaderId);
                var historyList = history.Select(h => new
                {
                    ActionDate = h.ActionDate.ToString("dd/MM/yyyy HH:mm"),
                    PerformedByUser = h.PerformedByUser.FullName,
                    PreviousState = h.PreviousState.Name,
                    NewState = h.NewState.Name,
                    ActionPerformed = h.ActionPerformed.Name,
                    Comments = h.Comments
                }).ToList();

                //return Json(historyList);
                return Json(new { data = historyList });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el historial del flujo para el encabezado {RequestFlowHeaderId}", requestFlowHeaderId);
                return StatusCode(500, "Error al obtener el historial del flujo.");
            }
        }

       

    }
}
