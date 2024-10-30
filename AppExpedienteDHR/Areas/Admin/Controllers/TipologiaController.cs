using Microsoft.AspNetCore.Mvc;
using AppExpedienteDHR.Core.ServiceContracts.Admin;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;
using AppExpedienteDHR.Core.ViewModels.Admin;

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
        public async Task<IActionResult> GetAllDerechos()
        {
            var derechos = await _derechoTipologiaService.GetDerechoTipologia();
            return Json(new { data = derechos }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
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
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                    await _derechoTipologiaService.InsertDerechoTipologia(model);
                else
                    await _derechoTipologiaService.UpdateDerechoTipologia(model);

                return Json(new { success = true });
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

            return PartialView("_EventoForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEvento(EventoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
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

            return PartialView("_EspecificidadForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditEspecificidad(EspecificidadViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                    await _especificidadTipologiaService.InsertEspecificidad(model);
                else
                    await _especificidadTipologiaService.UpdateEspecificidad(model);

                return Json(new { success = true });
            }
            return PartialView("_EspecificidadForm", model);
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
        public async Task<IActionResult> ExistDerechoCodeValidation([FromQuery(Name = "Codigo")] string codigo, [FromQuery(Name = "Id")] int id)
        {
            var derecho = await _derechoTipologiaService.GetDerechoTipologiaByCode(codigo);
            if (derecho != null && derecho.Id != id)
            {
                return Json("El código ya existe");
            }
            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> ExistEventoCodeValidation([FromQuery(Name = "Codigo")] string codigo, [FromQuery(Name = "Id")] int id)
        {
            var evento = await _eventoTipologiaService.GetEventoByCode(codigo);
            if (evento != null && evento.Id != id)
            {
                return Json("El código ya existe");
            }
            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> ExistEspecificidadCodeValidation([FromQuery(Name = "Codigo")] string codigo, [FromQuery(Name = "Id")] int id)
        {
            var especificidad = await _especificidadTipologiaService.GetEspecificidadByCode(codigo);
            if (especificidad != null && especificidad.Id != id)
            {
                return Json("El código ya existe");
            }
            return Json(true);
        }
        [HttpGet]
        public async Task<IActionResult> ExisteDescriptorCodeValidation([FromQuery(Name = "Codigo")] string codigo, [FromQuery(Name = "Id")] int id)
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
