using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class StateWfController : Controller
    {
        private readonly IStateWfService _stateWfService;
        private readonly IActionWfService _actionService;

        public StateWfController(IStateWfService stateWfService, IActionWfService actionService)
        {
            _stateWfService = stateWfService;
            _actionService = actionService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetStateForm([FromQuery] int flowId)
        {
            var viewModel = new StateWfViewModel
            {
                FlowWfId = flowId
            };
            return PartialView("_StateFormPartial", viewModel);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetState(int id)
        {
            var state = await _stateWfService.GetState(id);

            if (state == null)
            {
                return NotFound();
            }

            return PartialView("_StateFormPartial", state);
        }

        [HttpPost]
        public async Task<IActionResult> Save(StateWfViewModel stateViewModel)
        {
            if (stateViewModel.FlowWfId == 0)
            {
                return Json(new { success = false, message = "Debe seleccionar un flujo" });
            }

            if (ModelState.IsValid)
            {
                if (stateViewModel.Id == 0)
                {
                    await _stateWfService.CreateState(stateViewModel, stateViewModel.FlowWfId);
                }
                else
                {
                    await _stateWfService.UpdateState(stateViewModel);
                }
                return Json(new { success = true, message = "Estado guardado correctamente" });
            }
            return Json(new { success = false, message = "Error al guardar el estado" });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _stateWfService.DeleteState(id);
                return Json(new { success = true, message = "Estado eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el estado" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActionsTable([FromQuery] int stateId)
        {
            var actions = await _actionService.GetActions(stateId);
            return PartialView("_ActionsTablePartial", actions);
        }


        #region API CALLS
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStates(int id)
        {
            if (id == 0)
            {
                return Json(new { data = new List<StateWfViewModel>() });
            }
            var allObj = await _stateWfService.GetStates(id);

            return Json(new { data = allObj });
        }
        #endregion


    }
}
