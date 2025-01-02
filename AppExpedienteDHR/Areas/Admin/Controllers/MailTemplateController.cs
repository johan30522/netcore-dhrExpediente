using AppExpedienteDHR.Core.ServiceContracts.Admin;
using AppExpedienteDHR.Core.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppExpedienteDHR.Utils.Services;
using System.Reflection;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class MailTemplateController : Controller
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ITemplateRenderer _templateRenderer;



        public MailTemplateController(IEmailTemplateService emailTemplateService, ITemplateRenderer templateRenderer)
        {
            _emailTemplateService = emailTemplateService;
            _templateRenderer = templateRenderer;
        }





        public IActionResult Index()
        {
            return View();
        }

        // Crear o Editar Plantilla
        [HttpPost]
        public async Task<IActionResult> Save(EmailTemplateViewModel emailTemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                if (emailTemplateViewModel.Id == 0 || emailTemplateViewModel.Id == null)
                {
                    await _emailTemplateService.InsertEmailTemplate(emailTemplateViewModel);
                }
                else
                {
                    await _emailTemplateService.UpdateEmailTemplate(emailTemplateViewModel);
                }
                return Json(new { success = true, message = "Operación realizada con éxito" });
            }
            return Json(new { success = false, message = "Error en la operación" });
        }

        // Eliminar Plantilla
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _emailTemplateService.DeleteEmailTemplate(id);
            if (success)
            {
                return Json(new { success = true, message = "Operación realizada con éxito" });
            }
            return Json(new { success = false, message = "Error en la operación" });
        }

        // Vista previa de la plantilla
        [HttpGet]
        public async Task<IActionResult> Preview(int id, string entityName)
        {
            try
            {
                var template = await _emailTemplateService.GetEmailTemplateById(id);
                if (template == null) return NotFound();

                // Obtener el tipo de la entidad usando el nombre proporcionado
                var entityType = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.Name == entityName);
                if (entityType == null) return BadRequest($"Entidad '{entityName}' no encontrada.");

                // Generar datos de ejemplo
                var exampleData = ExampleDataGenerator.GenerateExampleData(entityType);

                // Renderizar las plantillas
                var subject = await _templateRenderer.RenderAsync(template.SubjectTemplate, exampleData);
                var body = await _templateRenderer.RenderAsync(template.BodyTemplate, exampleData);

                return Json(new { subject, body });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        #region API CALLS
        // Endpoint para DataTables
        [HttpGet]
        [Authorize] // Permitir acceso a cualquier usuario autenticado
        public async Task<IActionResult> GetAll()
            {
                var allObj = await _emailTemplateService.GetAllEmailTemplates();
                return Json(new { data = allObj });

            }

            // Endpoint para obtener una plantilla por id
            [HttpGet]
            public async Task<IActionResult> GetById([FromQuery] int id)
            {
                var obj = await _emailTemplateService.GetEmailTemplateById(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return Json(obj);
            }



            #endregion

        }
    }
