using AppExpedienteDHR.Core.ServiceContracts.General;
using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.Denuncia.Controllers
{
    [Area("Denuncia")]
    [Route("Denuncia/[controller]/[action]")]
    public class SolicitudController : Controller
    {
        private readonly IPadronService _padronService;
        private readonly IProvinciaService _provinciaService;
        private readonly ICantonService _cantonService;
        private readonly IDistritoService _distritoService;

        public SolicitudController(IPadronService padronService, IProvinciaService provinciaService, ICantonService cantonService, IDistritoService distritoService)
        {
            _padronService = padronService;
            _provinciaService = provinciaService;
            _cantonService = cantonService;
            _distritoService = distritoService;
        }
       

        public IActionResult Index()
        {
            return View();
        }


        #region api calls

        //obtener el ciudadano por cedula
        [HttpGet("{cedula}")]
        public async Task<IActionResult> GetCiudadano(string cedula)
        {
            var ciudadano = await _padronService.GetCiudadano(cedula);

            if (ciudadano == null)
            {
                return NotFound();
            }

            return Json(ciudadano);
        }
        //Obtener todas las provincias
        //Obtener los cantones por provincia
        //Obtener los distritos por canton
            

      





        #endregion


    }
}
