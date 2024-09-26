using AppExpedienteDHR.Core.Models;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.Expediente.Controllers
{
    [Authorize]
    [Area("Expediente")]
    [Route("Expediente/[controller]/[action]")]
    public class SolicitudController : Controller
    {
        private readonly IExpedienteService _expedienteService;

        public SolicitudController(IExpedienteService expedienteService)
        {
            _expedienteService = expedienteService;
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
            {
                new Breadcrumb { Title = "Expedientes", Url = Url.Action("Index", "Solicitud"), IsActive = false },
                new Breadcrumb { Title = "Solicitud", Url = Url.Action("Create", "Solicitud"), IsActive = true }
            };
            var model = new ExpedienteViewModel();
            model.IsEdit = true;
            return View("ExpedienteForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ExpedienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                // si el modelo tiene un id, es porque es una edición
                if (model.Id > 0)
                {
                    await _expedienteService.UpdateExpediente(model);
                }
                else
                {
                    await _expedienteService.CreateExpediente(model);
                }

            }
            model.IsEdit = true;
            return View("ExpedienteForm", model);
        }
    }
}
