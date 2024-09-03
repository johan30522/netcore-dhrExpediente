using Microsoft.AspNetCore.Mvc;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ActionWfController : Controller
    {
        private readonly IActionWfService _actionWfService;
        private readonly IStateWfService _stateWfService;
        private readonly IActionRuleWfService _actionRuleWfService;

        public ActionWfController(IActionWfService actionWfService, IStateWfService stateWfService, IActionRuleWfService actionRuleWfService)
        {
            _actionWfService = actionWfService;
            _stateWfService = stateWfService;
            _actionRuleWfService = actionRuleWfService;
        }
        


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetActionForm([FromQuery] int stateId)
        {
            StateWfViewModel state = await _stateWfService.GetState(stateId);
            if (state == null)
            {
                return NotFound();
            }
            var viewModel = new ActionWfViewModel
            {
                StateId = stateId,
                FlowId = state.FlowWfId
            };
            return PartialView("_ActionFormPartial", viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAction(int id)
        {
            var action = await _actionWfService.GetAction(id);
            if (action == null)
            {
                return NotFound();
            }
            StateWfViewModel state = await _stateWfService.GetState(action.StateId);
            if (state == null)
            {
                return NotFound();
            }
            action.FlowId = state.FlowWfId;



            return PartialView("_ActionFormPartial", action);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAction(ActionWfViewModel actionViewModel)
        {
            if (ModelState.IsValid)
            {
                if (actionViewModel.Id == 0)
                {
                    await _actionWfService.CreateAction(actionViewModel);
                }
                else
                {
                    await _actionWfService.UpdateAction(actionViewModel);
                }

                return Json(new 
                { 
                    success = true,
                    stateId = actionViewModel.StateId
                });
            }
            return PartialView("_ActionFormPartial", actionViewModel);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteAction(int id)
        {
            try
            {
                await _actionWfService.DeleteAction(id);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetActionRulesTable([FromQuery] int actionId)
        {
            var rules = await _actionRuleWfService.GetActionRules(actionId);
            return PartialView("_RulesTablePartial", rules);
        }

        #endregion




    }
}
