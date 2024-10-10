
using AppExpedienteDHR.Core.ViewModels.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppExpedienteDHR.Utils.Validation;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AppExpedienteDHR.Core.Models;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class FlowWfController : Controller
    {
        private readonly IFlowWfService _flowWfService;
        private readonly IGroupWfService _groupWfService;
        private readonly IStateWfService _stateWfService;


        public FlowWfController(IFlowWfService flowWfService, IGroupWfService groupWfService, IStateWfService stateWfService)
        {
            _flowWfService = flowWfService;
            _groupWfService = groupWfService;
            _stateWfService = stateWfService;
        }

        public IActionResult Index()
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
                {
                    new Breadcrumb { Title = "Configuración", Url = Url.Action("Index", "FlowWf"), IsActive = true }
                };
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
                {
                    new Breadcrumb { Title = "Configuración", Url = Url.Action("Index", "FlowWf"), IsActive = false },
                    new Breadcrumb { Title = "Crear Flujo", Url = Url.Action("Create", "FlowWf"), IsActive = true }
                };

            FlowFormViewModel viewModel = new FlowFormViewModel
            {
                Flow = new FlowWfViewModel()
            };
            return View("FlowForm", viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
                {
                    new Breadcrumb { Title = "Configuración", Url = Url.Action("Index", "FlowWf"), IsActive = false },
                    new Breadcrumb { Title = "Editar Flujo", Url = Url.Action("Edit", "FlowWf"), IsActive = true }
                };
            FlowWfViewModel flowViewModel = await _flowWfService.GetFlow(id);
            if (flowViewModel == null)
            {
                return NotFound();
            }
            FlowFormViewModel viewModel = new FlowFormViewModel
            {
                Flow = flowViewModel
            };
            return View("FlowForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(FlowFormViewModel flowFormViewModel)
        {



                ModelStateHelper.RemoveModelStateForObject(ModelState, "Group");
                ModelStateHelper.RemoveModelStateForObject(ModelState, "State");
       


            if (ModelState.IsValid)
            {
                FlowWfViewModel flowViewModel = flowFormViewModel.Flow;
                if (flowFormViewModel.Flow.Id == 0)
                {
                    flowViewModel = await _flowWfService.CreateFlow(flowViewModel);
                }
                else
                {
                    flowViewModel = await _flowWfService.UpdateFlow(flowFormViewModel.Flow);
                }
                flowFormViewModel.Flow = flowViewModel;

                //return View("FlowForm", flowFormViewModel);
                return RedirectToAction("Edit", new { id = flowViewModel.Id });

            }
            return View("FlowForm", flowFormViewModel);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _flowWfService.DeleteFlow(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _flowWfService.GetFlows();

            return Json(new { data = allObj });
        }






        #endregion


    }
}
