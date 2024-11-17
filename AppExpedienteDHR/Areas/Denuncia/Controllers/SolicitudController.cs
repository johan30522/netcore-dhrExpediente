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
using AppExpedienteDHR.Core.Services.Dhr;

namespace AppExpedienteDHR.Areas.Denuncia.Controllers
{
    [Authorize]
    [Area("Denuncia")]
    [Route("Denuncia/[controller]/[action]")]
    public class SolicitudController : Controller
    {
       
        private readonly IDenunciaService _denunciaService;
        private readonly IAdjuntoService _adjuntoService;
        private readonly ILockRecordService _lockRecordService;
        private readonly IUserService _userService;
        private readonly ILoadFormPropsService _loadFormPropsService;



        public SolicitudController(
           
            IDenunciaService denunciaService,
            IAdjuntoService adjuntoService,
            ILockRecordService lockRecordService,
            IUserService userService,
            ILoadFormPropsService loadFormPropsService
            )
        {
            
            _denunciaService = denunciaService;
            _adjuntoService = adjuntoService;
            _lockRecordService = lockRecordService;
            _userService = userService;
            _loadFormPropsService = loadFormPropsService;
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
        [AllowAnonymous]
        public async Task<IActionResult> DenunciaWeb()
        {
            var model = new DenunciaViewModel();
            model = await _loadFormPropsService.LoadCatalogsForDenuncia(model);
            model.IsEdit = true;
            return View("DenunciaWeb", model);
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
            model = await _loadFormPropsService.LoadCatalogsForDenuncia(model);
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
                model = await _loadFormPropsService.LoadCatalogsForDenuncia(model);
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
            model = await _loadFormPropsService.LoadCatalogsForDenuncia(model);
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

            if (lockid == 0)
            {
                TempData["ErrorMessage"] = "El registro está siendo editado por otro usuario";
                return RedirectToAction("Info", new { id });
            }

            ViewData["Breadcrumbs"] = new List<Breadcrumb>
                    {
                        new Breadcrumb { Title = "Denuncias", Url = Url.Action("Index", "Solicitud"), IsActive = false },
                        new Breadcrumb { Title = "Editar Denuncia", Url = Url.Action("Edit", "Solicitud", new { id }), IsActive = true }
                    };

            model = await _loadFormPropsService.LoadCatalogsForDenuncia(model);

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
        public async Task<IActionResult> GenerateExpediente(int id)
        {
            var existeDenuncia = await _denunciaService.GetDenuncia(id);
            if (existeDenuncia == null)
            {
                return NotFound();
            }
            var result = await _denunciaService.CreateExpediente(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
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

                var anexoExpediente = await _denunciaService.GetAnexoById(anexoId);



                // Buscar el archivo adjunto por su ID
                var anexo = await _adjuntoService.GetAnexo(anexoExpediente.AdjuntoId);
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


       
        #endregion


    }
}
