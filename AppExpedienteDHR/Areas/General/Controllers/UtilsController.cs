using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.ServiceContracts.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.General.Controllers
{
    [Authorize]
    [Area("General")]
    [Route("General/[controller]/[action]")]
    public class UtilsController : Controller
    {
        private readonly IPadronService _padronService;
        private readonly ICantonService _cantonService;
        private readonly IDistritoService _distritoService;

        public UtilsController(IPadronService padronService, ICantonService cantonService, IDistritoService distritoService)
        {
            _padronService = padronService;
            _cantonService = cantonService;
            _distritoService = distritoService;
        }


        [HttpPost]
        public IActionResult SetPreviousView([FromBody] string previousView)
        {
            if (previousView == null) {
                return Ok();
            }
            HttpContext.Session.SetString("PreviousView", previousView);
            return Ok();
        }

        //Obtener el ciudadano por cedula
        [HttpGet]
        public async Task<IActionResult> GetCiudadano([FromQuery] string cedula)
        {
            var ciudadano = await _padronService.GetCiudadano(cedula);

            if (ciudadano == null)
            {
                // Retorna un mensaje de error personalizado con estado 404
                return NotFound(new { error = "Ciudadano no encontrado" });
            }
            return Json(ciudadano);
        }

        //Obtener los cantones por provincia
        [HttpGet]
        public async Task<IActionResult> GetCantones([FromQuery] int provinciaId)
        {
            var cantones = await _cantonService.GetAllCantones(provinciaId);

            if (cantones == null)
            {
                // return empty list
                return Json(new List<Canton>());
            }

            return Json(cantones);
        }

        //Obtener los distritos por canton

        [HttpGet]
        public async Task<IActionResult> GetDistritos([FromQuery] int cantonId)
        {
            var distritos = await _distritoService.GetAllDistritos(cantonId);

            if (distritos == null)
            {
                // return empty list
                return Json(new List<Distrito>());
            }

            return Json(distritos);
        }
    }
}
