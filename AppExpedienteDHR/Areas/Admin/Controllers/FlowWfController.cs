using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.Services;
using AppExpedienteDHR.Core.ViewModels.User;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppExpedienteDHR.Utils.Validation;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    //[Authorize(Roles = "Admin")]
    public class FlowWfController : Controller
    {
        private readonly IFlowWfService _flowWfService;
        private readonly IGroupWfService _groupWfService;

        public FlowWfController(IFlowWfService flowWfService, IGroupWfService groupWfService)
        {
            _flowWfService = flowWfService;
            _groupWfService = groupWfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            FlowFormViewModel viewModel = new FlowFormViewModel
            {
                Flow = new FlowWfViewModel()
            };
            return View("FlowForm", viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
            {
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

            if (flowFormViewModel.Flow.Id == 0)
            {
                // remover la validaciones de las propiedades que no se deben validar
                //RemoveModelStateForObject("Group");
                //RemoveModelStateForObject("State");
                // Remover la validación para todas las propiedades del objeto Group y State
                ModelStateHelper.RemoveModelStateForObject(ModelState, "Group");
                ModelStateHelper.RemoveModelStateForObject(ModelState, "State");
            }


            if (ModelState.IsValid)
            {
                FlowWfViewModel flowViewModel = flowFormViewModel.Flow;
                if (flowFormViewModel.Flow.Id == 0)
                {
                    flowViewModel = await _flowWfService.CreateFlow(flowViewModel);
                }
                else
                {
                    flowViewModel=await _flowWfService.UpdateFlow(flowFormViewModel.Flow);
                }
                flowFormViewModel.Flow = flowViewModel;

                return View("FlowForm", flowFormViewModel);

            }
            return View("FlowForm", flowFormViewModel);
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _flowWfService.GetFlows();

            return Json(new { data = allObj });
        }

        // flowId se debe obtener desde la url ejem: /Admin/FlowWf/GetGroups/1
        //[HttpGet("Admin/FlowWf/GetGroups/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroups(int id)
        {
            if (id == 0)
            {
                return Json(new { data = new List<GroupWfViewModel>() });
            }
            var allObj = await _groupWfService.GetGroups(id);

            return Json(new { data = allObj });
        }


        #endregion

        
    }
}
