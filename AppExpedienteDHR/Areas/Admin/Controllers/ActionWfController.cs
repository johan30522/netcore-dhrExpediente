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

        public ActionWfController(IActionWfService actionWfService)
        {
            _actionWfService = actionWfService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetActionForm([FromQuery] int stateId)
        {
            var viewModel = new ActionWfViewModel
            {
                StateId = stateId
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

        [HttpPost]
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




    }
}
