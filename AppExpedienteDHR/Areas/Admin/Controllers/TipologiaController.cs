using Microsoft.AspNetCore.Mvc;
using AppExpedienteDHR.Core.ServiceContracts.Admin;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;
using AppExpedienteDHR.Core.ViewModels.Admin;
using AppExpedienteDHR.Utils.Validation;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    //[Authorize(Roles = "Admin")]
    public class TipologiaController : Controller
    {
        private readonly IEventoTipologiaService _eventoTipologiaService;
        private readonly IDescriptorTipologiaService _descriptorTipologiaService;
        private readonly IDerechoTipologiaService _derechoTipologiaService;
        private readonly IEspecificidadTipologiaService _especificidadTipologiaService;



        public TipologiaController(
            IEventoTipologiaService eventoTipologiaService,
            IDescriptorTipologiaService descriptorTipologiaService,
            IDerechoTipologiaService derechoTipologiaService,
            IEspecificidadTipologiaService especificidadTipologiaService
        )
        {
            _eventoTipologiaService = eventoTipologiaService;
            _descriptorTipologiaService = descriptorTipologiaService;
            _derechoTipologiaService = derechoTipologiaService;
            _especificidadTipologiaService = especificidadTipologiaService;
        }
        public IActionResult Derechos()
        {
            return View();
        }
        public IActionResult Eventos()
        {
            return View();
        }
        public IActionResult Especificidades()
        {
            return View();
        }


        #region Api Calls
        // Api para obtener todos los derechos
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDerechos()
        {
            var derechos = await _derechoTipologiaService.GetDerechoTipologia();
            return Json(new { data = derechos });
        }

        [HttpGet("{derechoId}")]
        public async Task<IActionResult> GetEventosPorDerecho(int derechoId)
        {
            // Cargar los eventos para un derecho específico
            var eventos = await _eventoTipologiaService.GetEventos(derechoId);
            return Json(eventos);
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetEspecificidadesPorEvento(int eventoId)
        {
            // Cargar las especificidades para un evento específico
            var especificidades = await _especificidadTipologiaService.GetEspecificidades(eventoId);
            return Json(especificidades);
        }
        #endregion

        #region Create & Edit

        // Obtener vista parcial para crear o editar un Derecho
        [HttpGet]
        public async Task<IActionResult> CreateOrEditDerecho(int? id)
        {
            DerechoViewModel model = id.HasValue
                ? await _derechoTipologiaService.GetDerechoTipologiaById(id.Value)
                : new DerechoViewModel();

            return PartialView("_DerechoForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDerecho(DerechoViewModel model)
        {
            // si el id es null entonces se esta creando un nuevo derecho
            if (model.Id==null)
            {
                ModelStateHelper.RemoveModelStateForObject(ModelState, "Id");

            }
            if (ModelState.IsValid)
            {
                if (model.Id == 0 || model.Id == null)
                    await _derechoTipologiaService.InsertDerechoTipologia(model);
                else
                    await _derechoTipologiaService.UpdateDerechoTipologia(model);

                return Json(new { success = true });
            }else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            }
            return PartialView("_DerechoForm", model);
        }

        // Obtener vista parcial para crear o editar un Evento
        [HttpGet]
        public async Task<IActionResult> CreateOrEditEvento([FromQuery] int? id, [FromQuery] int derechoId)
        {

            EventoViewModel model = id.HasValue
                ? await _eventoTipologiaService.GetEventoById(id.Value)
                : new EventoViewModel { DerechoId = derechoId };

            // Asegurarse de que el DerechoId esté en el modelo en caso de edición
            if (model.DerechoId.HasValue)
            {
                ViewData["DerechoId"] = model.DerechoId;
            }

            return PartialView("_EventoForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEvento(EventoViewModel model)
        {
            if (model.Id == null)
            {
                ModelStateHelper.RemoveModelStateForObject(ModelState, "Id");
            }
            if (ModelState.IsValid)
            {
                if (model.Id == 0 || model.Id == null)
                    await _eventoTipologiaService.InsertEvento(model);
                else
                    await _eventoTipologiaService.UpdateEvento(model);

                return Json(new { success = true });
            }
            return PartialView("_EventoForm", model);
        }

        // Obtener vista parcial para crear o editar una Especificidad
        [HttpGet]
        public async Task<IActionResult> CreateOrEditEspecificidad(int? id, int eventoId)
        {
            EspecificidadViewModel model = id.HasValue
                ? await _especificidadTipologiaService.GetEspecificidadById(id.Value)
                : new EspecificidadViewModel { EventoId = eventoId };

            if (model.EventoId.HasValue)
            {
                ViewData["EventoId"] = model.EventoId;
            }

            return PartialView("_EspecificidadForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEspecificidad(EspecificidadViewModel model)
        {
            if (model.Id == null)
            {
                ModelStateHelper.RemoveModelStateForObject(ModelState, "Id");
            }
            if (ModelState.IsValid)
            {
                if (model.Id == 0 || model.Id == null)
                    await _especificidadTipologiaService.InsertEspecificidad(model);
                else
                    await _especificidadTipologiaService.UpdateEspecificidad(model);

                return Json(new { success = true });
            }
            return PartialView("_EspecificidadForm", model);
        }



        // Delete item por nivel de tipologia (Derecho, Evento, Especificidad)
        [HttpPost]
        public async Task<IActionResult> DeleteTipologia([FromQuery] int id, [FromQuery] string tipologia)
        {
            if((id == 0) || string.IsNullOrEmpty(tipologia))
            {
                return Json(new { success = false });
            }
            switch (tipologia)
            {
                case "Derecho":
                    await _derechoTipologiaService.DeleteDerechoTipologia(id);
                    break;
                case "Evento":
                    await _eventoTipologiaService.DeleteEvento(id);
                    break;
                case "Especificidad":
                    await _especificidadTipologiaService.DeleteEspecificidad(id);
                    break;
            }

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> ManageDescriptors(int eventoId)
        {
            var descriptors = await _descriptorTipologiaService.GetDescriptors(eventoId);
            var model = new DescriptorListViewModel
            {
                EventoId = eventoId,
                Descriptores = descriptors.ToList()
            };
            return PartialView("_ManageDescriptores", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDescriptorsForEvent(int eventoId)
        {
            var descriptors = await _descriptorTipologiaService.GetDescriptors(eventoId);
            return PartialView("_DescriptorTableBody", descriptors);
        }


        [HttpPost]
        public async Task<IActionResult> SaveDescriptor(DescriptorViewModel descriptor)
        {
            if (descriptor.Id == 0 || descriptor.Id ==null)
                await _descriptorTipologiaService.InsertDescriptor(descriptor);
            else
                await _descriptorTipologiaService.UpdateDescriptor(descriptor);

            return Json(new { success = true, eventoId = descriptor.EventoId });
        }

        [HttpGet]
        public async Task<IActionResult> GetDescriptor(int id)
        {
            var descriptor = await _descriptorTipologiaService.GetDescriptorById(id);
            if (descriptor == null)
                return NotFound();

            return Json(descriptor);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDescriptor(int id)
        {
            var result = await _descriptorTipologiaService.DeleteDescriptor(id);
            return Json(new { success = result });
        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> DeleteDerecho(int id)
        {
            await _derechoTipologiaService.DeleteDerechoTipologia(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            await _eventoTipologiaService.DeleteEvento(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEspecificidad(int id)
        {
            await _especificidadTipologiaService.DeleteEspecificidad(id);
            return Json(new { success = true });
        }

        #endregion

        #region validations 
        [HttpGet]
        public async Task<IActionResult> ExistDerechoCodeValidation([FromQuery(Name = "Codigo")] string codigo, [FromQuery(Name = "Id")] int? id)
        {
            var derecho = await _derechoTipologiaService.GetDerechoTipologiaByCode(codigo);
            if (derecho != null && derecho.Id != id)
            {
                return Json("El código ya existe");
            }
            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> ExistEventoCodeValidation([FromQuery(Name = "Codigo")] string codigo, [FromQuery(Name = "Id")] int? id)
        {
            var evento = await _eventoTipologiaService.GetEventoByCode(codigo);
            if (evento != null && evento.Id != id)
            {
                return Json("El código ya existe");
            }
            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> ExistEspecificidadCodeValidation([FromQuery(Name = "Codigo")] string codigo, [FromQuery(Name = "Id")] int? id)
        {
            var especificidad = await _especificidadTipologiaService.GetEspecificidadByCode(codigo);
            if (especificidad != null && especificidad.Id != id)
            {
                return Json("El código ya existe");
            }
            return Json(true);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDescriptorsByEventoId([FromQuery(Name = "eventoId")] int eventoId)
        {
            // Obtén los descriptores asociados al EventoId (filtrados por el evento correspondiente)
            var descriptors = await _descriptorTipologiaService.GetDescriptors(eventoId);

            // Retorna la lista en formato JSON
            return Json(descriptors);

        }
        [HttpGet]
        public async Task<IActionResult> ExisteDescriptorCodeValidation([FromQuery(Name = "Codigo")] string codigo, [FromQuery(Name = "Id")] int? id)
        {
            var descriptor = await _descriptorTipologiaService.GetDescriptorByCode(codigo);
            if (descriptor != null && descriptor.Id != id)
            {
                return Json("El código ya existe");
            }
            return Json(true);
        }
        
        #endregion

    }
}
