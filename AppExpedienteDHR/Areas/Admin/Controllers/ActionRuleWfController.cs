using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class ActionRuleWfController : Controller
    {
        private readonly IActionRuleWfService _actionRuleWfService;
        private readonly IActionWfService _actionWfService;

        public ActionRuleWfController(IActionRuleWfService actionRuleWfService, IActionWfService actionWfService)
        {
            _actionRuleWfService = actionRuleWfService;
            _actionWfService = actionWfService;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetActionRuleForm([FromQuery] int actionId)
        {
            var viewModel = new ActionRuleWfViewModel
            {
                ActionId = actionId
            };
            return PartialView("_RuleFormPartial", viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> GetActionRule([FromQuery] int ruleId)
        {
            var actionRule = await _actionRuleWfService.GetActionRule(ruleId);

            if (actionRule == null)
            {
                return NotFound();
            }

            return PartialView("_RuleFormPartial", actionRule);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRule(ActionRuleWfViewModel ruleViewModel)
        {
            if (ModelState.IsValid)
            {
                if (ruleViewModel.Id == 0)
                {
                    await _actionRuleWfService.CreateActionRule(ruleViewModel);
                }
                else
                {
                    await _actionRuleWfService.UpdateActionRule(ruleViewModel);
                }
                return Json(new { success = true });
            }
            return PartialView("_RuleFormPartial", ruleViewModel);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            try
            {
                await _actionRuleWfService.DeleteActionRule(id);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }


    }
}
