using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.ServiceContracts.General;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using Microsoft.AspNetCore.Mvc;
using AppExpedienteDHR.Utils.Validation;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;

using Serilog;
using AppExpedienteDHR.Core.Models;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.User;
using Microsoft.AspNetCore.Authorization;

namespace AppExpedienteDHR.Areas.Denuncia.Controllers
{
    [Authorize]
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
        private readonly IAdjuntoService _adjuntoService;
        private readonly ILockRecordService _lockRecordService;
        private readonly IUserService _userService;



        public SolicitudController(
            IPadronService padronService,
            IProvinciaService provinciaService,
            ICantonService cantonService,
            IDistritoService distritoService,
            ITipoIdentificacionService tipoIdentificacionService,
            ISexoService sexoService,
            IEstadoCivilService estadoCivilService,
            IPaisService paisService,
            IEscolaridadService escolaridadService,
            IDenunciaService denunciaService,
            IAdjuntoService adjuntoService,
            ILockRecordService lockRecordService,
            IUserService userService
            )
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
            _adjuntoService = adjuntoService;
            _lockRecordService = lockRecordService;
            _userService = userService;
        }


        public IActionResult Index()
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
                {
                    new Breadcrumb { Title = "Denuncias", Url = Url.Action("Index", "Solicitud"), IsActive = true }
                };
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
            {
                new Breadcrumb { Title = "Denuncias", Url = Url.Action("Index", "Solicitud"), IsActive = false },
                new Breadcrumb { Title = "Creación", Url = Url.Action("Create", "Solicitud"), IsActive = true }
            };
            var model = new DenunciaViewModel();
            await LoadCatalogs(model);
            model.IsEdit = true;
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
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
            {
                new Breadcrumb { Title = "Denuncias", Url = Url.Action("Index", "Solicitud"), IsActive = false },
                new Breadcrumb { Title = "Información Denuncia", Url = Url.Action("Info", "Solicitud", new { id }), IsActive = true }
            };
            var model = await _denunciaService.GetDenuncia(id);
            await LoadCatalogs(model);
            model.IsEdit = false;
            return View("DenunciaInfo", model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            // Bloquear el registro antes de permitir la edición
            var model = await _denunciaService.GetDenuncia(id);
            if (model == null)
            {
                return NotFound();
            }
            UserViewModel curUser=await _userService.GetCurrentUser();
            if (curUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // verificar si el registro esta bloqueado


            var (isLocked, lockedByUserName) = await _lockRecordService.IsRecordLocked(id, "Denuncia");
            if (isLocked)
            {
                TempData["ErrorMessage"] = "El registro está siendo editado por: " + lockedByUserName ;
                return RedirectToAction("Info", new { id });
            }

            int lockid = await _lockRecordService.LockRecord(model.Id, "Denuncia",curUser.Id);

            ViewData["Breadcrumbs"] = new List<Breadcrumb>
                    {
                        new Breadcrumb { Title = "Denuncias", Url = Url.Action("Index", "Solicitud"), IsActive = false },
                        new Breadcrumb { Title = "Editar Denuncia", Url = Url.Action("Edit", "Solicitud", new { id }), IsActive = true }
                    };

            await LoadCatalogs(model);

            // Permitir la edición del modelo
            model.IsEdit = true;
            model.LockedRecordId = lockid;

            return View("DenunciaInfo", model); // Usamos la misma vista del formulario, pero en modo editable
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

            //redirect to info with the same id
            return RedirectToAction("Edit", new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAnexo(int id, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {

                await _denunciaService.AgregarAnexoDenuncia(id, file);

                return Ok();
            }
            return BadRequest("El archivo es requerido.");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAnexo(int anexoId)
        {
            try
            {
                await _adjuntoService.EliminarArchivoAsync(anexoId);
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "Error eliminando el anexo");
                return BadRequest("Error eliminando el anexo.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DescargarAnexo(int anexoId)
        {
            try
            {
                // Buscar el archivo adjunto por su ID
                var anexo = await _adjuntoService.GetAnexo(anexoId);
                if (anexo == null)
                {
                    return NotFound();
                }

                // Obtener el archivo como un array de bytes
                var archivoBytes = await _adjuntoService.DescargarArchivoAsync(anexo.Ruta);

                // Devolver el archivo como una descarga
                return File(archivoBytes, "application/octet-stream", anexo.NombreOriginal);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "Error al descargar el anexo con ID: {AnexoId}", anexoId);
                return BadRequest("Error al descargar el archivo.");
            }
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
        /**
         * Método que se encarga de obtener todas las denuncias paginadas
         * Permitiendo la búsqueda y ordenamiento de las mismas
         * 
         * @return IActionResult
         */

        [HttpPost]
        public async Task<IActionResult> GetAllDenuncias()
        {
            var draw = Request.Form["draw"].FirstOrDefault(); // Obtiene el número de la petición, para enviarlo de vuelta
            var start = Request.Form["start"].FirstOrDefault(); // Obtiene el inicio de la paginación
            var length = Request.Form["length"].FirstOrDefault(); // Obtiene la cantidad de registros a mostrar

            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();// Obtiene la columna a ordenar
            if (sortColumn == "denuncianteNombre")
            {
                sortColumn = "denunciante.Nombre";
            }
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();// Obtiene la dirección de ordenamiento
            var searchValue = Request.Form["search[value]"].FirstOrDefault(); // Obtiene el valor de búsqueda

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
