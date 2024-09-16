using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.ServiceContracts.General;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using Microsoft.AspNetCore.Mvc;
using AppExpedienteDHR.Utils.Validation;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;

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
        private readonly ITipoIdentificacionService _tipoIdentificacionService;
        private readonly ISexoService _sexoService;
        private readonly IEstadoCivilService _estadoCivilService;
        private readonly IPaisService _paisService;
        private readonly IEscolaridadService _escolaridadService;
        private readonly IDenunciaService _denunciaService;



        public SolicitudController(IPadronService padronService, IProvinciaService provinciaService, ICantonService cantonService, IDistritoService distritoService, ITipoIdentificacionService tipoIdentificacionService, ISexoService sexoService, IEstadoCivilService estadoCivilService, IPaisService paisService, IEscolaridadService escolaridadService, IDenunciaService denunciaService)
        {
            _padronService = padronService;
            _provinciaService = provinciaService;
            _cantonService = cantonService;
            _distritoService = distritoService;
            _tipoIdentificacionService = tipoIdentificacionService;
            _sexoService = sexoService;
            _estadoCivilService = estadoCivilService;
            _paisService = paisService;
            _escolaridadService = escolaridadService;
            _denunciaService = denunciaService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new DenunciaViewModel();
            await LoadCatalogs(model);
            return View("DenunciaForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DenunciaViewModel model)
        {
            if (!model.IncluyePersonaAfectada)
            {
                ModelStateHelper.RemoveModelStateForObject(ModelState, "PersonaAfectada");

            }
            if (ModelState.IsValid)
            {
                //inicia guardado
                var denuncia = await _denunciaService.CreateDenuncia(model);
            }
            else
            {
                await LoadCatalogs(model);
                return View("DenunciaForm", model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Info(int id)
        {
            var model = await _denunciaService.GetDenuncia(id);
            await LoadCatalogs(model);
            return View("DenunciaInfo", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(DenunciaViewModel model)
        {
            if (!model.IncluyePersonaAfectada)
            {
                ModelStateHelper.RemoveModelStateForObject(ModelState, "PersonaAfectada");

            }
            if (ModelState.IsValid)
            {
                await _denunciaService.UpdateDenuncia(model);
            }
            await LoadCatalogs(model);
            return View("DenunciaFormEdit", model);
        }



        private async Task LoadCatalogs(DenunciaViewModel model)
        {
            var provincias = await _provinciaService.GetAllProvincias();
            var sexos = await _sexoService.GetAllSexos();
            var estadosCiviles = await _estadoCivilService.GetAllEstadoCivil();
            var tiposIdentificacion = await _tipoIdentificacionService.GetAllTipoIdentificaciones();
            var paises = await _paisService.GetAllPaises();
            var escolaridades = await _escolaridadService.GetAllEscolaridades();

            model.ListTiposIdentificacion = tiposIdentificacion;
            model.ListSexos = sexos;
            model.ListEstadosCiviles = estadosCiviles;
            model.ListPaises = paises;
            model.ListEscolaridades = escolaridades;
            model.ListProvincias = provincias;
        }


        #region api calls

        [HttpPost]
        public async Task<IActionResult> GetAllDenuncias()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();

            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
            if(sortColumn == "denuncianteNombre")
            {
                sortColumn = "denunciante.Nombre";
            }
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 10;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int pageIndex = skip / pageSize;

            // Llamar al servicio para obtener las denuncias paginadas
            var (denuncias, totalItems) = await _denunciaService.GetDenunciasPaginadas(
                pageIndex: pageIndex,
                pageSize: pageSize,
                searchValue: searchValue,
                sortColumn: sortColumn,
                sortDirection: sortColumnDirection);

            // Preparar el objeto de retorno para DataTables
            var jsonData = new
            {
                draw = draw,
                recordsFiltered = totalItems,
                recordsTotal = totalItems,
                data = denuncias
            };

            return Ok(jsonData);

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








        #endregion


    }
}
