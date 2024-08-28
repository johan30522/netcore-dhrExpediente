using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    //[Authorize(Roles = "Admin")]
    public class GroupWfController : Controller
    {
        private readonly IGroupWfService _groupWfService;

        public GroupWfController(IGroupWfService groupWfService)
        {
            _groupWfService = groupWfService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetGroupForm([FromQuery] int flowId)
        {
            var viewModel = new GroupWfViewModel
            {
                FlowWfId = flowId
            };
            return PartialView("_GroupFormPartial", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroup([FromQuery] int id)
        {
            var group = await _groupWfService.GetGroup(id);
            return PartialView("_GroupFormPartial", group);
        }

        [HttpPost]
        public async Task<IActionResult> SaveGroup(GroupWfViewModel groupViewModel)
        {
            if(groupViewModel.FlowWfId == 0)
            {
                return Json(new { success = false, message = "Debe seleccionar un flujo" });
            }
            
            if (ModelState.IsValid)
            {
                if (groupViewModel.Id == 0)
                {
                    await _groupWfService.CreateGroup(groupViewModel, groupViewModel.FlowWfId);
                }
                else
                {
                    await _groupWfService.UpdateGroup(groupViewModel);
                }

                return Json(new { success = true });
            }

            return PartialView("_GroupFormPartial", groupViewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            await _groupWfService.DeleteGroup(id);
            return Json(new { success = true });
        }




    }
}
