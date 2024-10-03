﻿using AppExpedienteDHR.Core.Models;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AppExpedienteDHR.Core.Services.Dhr;
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
        private readonly IWorkflowService _workflowService;
        private readonly Serilog.ILogger _logger;

        public SolicitudController(IExpedienteService expedienteService, IWorkflowService workflowService, Serilog.ILogger logger)
        {
            _expedienteService = expedienteService;
            _workflowService = workflowService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
            {
                new Breadcrumb { Title = "Expedientes", Url = Url.Action("Index", "Solicitud"), IsActive = true }
            };

            return View();
        }
        [HttpGet]
        public IActionResult ByState()
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
            {
                new Breadcrumb { Title = "Por Estado", Url = Url.Action("ByState", "Solicitud"), IsActive = true }
            };

            return View();
        }
        [HttpGet]
        public IActionResult MyExpedientes()
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
            {
                new Breadcrumb { Title = "Mis Expedientes", Url = Url.Action("MyExpedientes", "Solicitud"), IsActive = true }
            };

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
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            // se obtiene el encabezado de Flujo de la solicitud
            var flowRequestHeader = await _workflowService.GetFlowRequestHeader(id, "Expediente");
            if (flowRequestHeader == null)
            {
                return RedirectToAction("Index");
            }
            // verifica si el usuario actual tiene acciones disponibles para la solicitud
            var hasActions = await _workflowService.HasActions(flowRequestHeader.FlowId, flowRequestHeader.CurrentStateId);
            if (!hasActions)
            {
                TempData["ErrorMessage"] = "No tiene acciones disponibles para esta solicitud.";
                return RedirectToAction("Info", new { id = id });
            }
            // Se agregan las migas de pan
            //AddBreadcrumbs(id, "Edit");
            // Obtén el valor de la vista previa desde sessionStorage en el cliente
            string previousView = HttpContext.Session.GetString("PreviousView") ?? "Index";
            // Se agregan las migas de pan
            AddBreadcrumbs(id, "Edit", previousView);

            // Obtener el Referer

            // se obtiene el expediente
            var model = await _expedienteService.GetExpediente(id);
            //Agregan campos de control
            model.FlowWfId = flowRequestHeader.FlowId;
            model.FlowHeaderWfId = flowRequestHeader.Id;
            model.StateWfId = flowRequestHeader.CurrentStateId;
            model.StateWfName = flowRequestHeader.CurrentState.Name;
            model.FlowWfName = flowRequestHeader.Flow.Name;
            model.CreatedDate = flowRequestHeader.CreatedDate;


            model.IsEdit = true;
            return View("ExpedienteForm", model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Info(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            // se obtiene el encabezado de Flujo de la solicitud
            var flowRequestHeader = await _workflowService.GetFlowRequestHeader(id, "Expediente");
            if (flowRequestHeader == null)
            {
                return RedirectToAction("Index");
            }
            string previusViewFrom = HttpContext.Session.GetString("PreviousView");
            string previousView = HttpContext.Session.GetString("PreviousView") ?? "Index";
            // Se agregan las migas de pan
            AddBreadcrumbs(id, "Info", previousView);

            //AddBreadcrumbs(id, "Info");
            var model = await _expedienteService.GetExpediente(id);
            //Agregan campos de control
            model.FlowWfId = flowRequestHeader.FlowId;
            model.FlowHeaderWfId = flowRequestHeader.Id;
            model.StateWfId = flowRequestHeader.CurrentStateId;
            model.StateWfName = flowRequestHeader.CurrentState.Name;
            model.FlowWfName = flowRequestHeader.Flow.Name;
            model.CreatedDate = flowRequestHeader.CreatedDate;

            model.IsEdit = false;
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
                    var expediente = await _expedienteService.CreateExpediente(model);
                    model.Id = expediente.Id;
                }

            }
            model.IsEdit = true;
            return RedirectToAction("Edit", new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> GetAllExpedientes()
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
            var (denuncias, totalItems) = await _expedienteService.GetExpedientesPaginadas(
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

        //// permite agregar las migas de pan
        //private void AddBreadcrumbs(int? id = 0, string? fromView = "")
        //{
        //    var referer = Request.Headers["Referer"].ToString();
        //    var breadcrumbs = new List<Breadcrumb>();

        //    if (referer.Contains("ByState"))
        //    {
        //        breadcrumbs.Add(new Breadcrumb { Title = "Expedientes por Estado", Url = Url.Action("ByState", "Por Estado"), IsActive = false });
        //    }
        //    else if (referer.Contains("MyExpedientes"))
        //    {
        //        breadcrumbs.Add(new Breadcrumb { Title = "Mis Expedientes", Url = Url.Action("MyExpedientes", "Mis Expedientes"), IsActive = false });
        //    }
        //    else
        //    {
        //        breadcrumbs.Add(new Breadcrumb { Title = "Expedientes", Url = Url.Action("Index", "Expedientes"), IsActive = false });
        //    }

        //    if (fromView == "Create")
        //        breadcrumbs.Add(new Breadcrumb { Title = "Crear", Url = Url.Action("Create", "Solicitud"), IsActive = true });
        //    else if (fromView == "Edit")
        //    {
        //        breadcrumbs.Add(new Breadcrumb { Title = "Editar", Url = Url.Action("Edit", "Solicitud", new { id = id }), IsActive = false });
        //    }
        //    else
        //    {
        //        breadcrumbs.Add(new Breadcrumb { Title = "Informacion", Url = Url.Action("Info", "Solicitud", new { id = id }), IsActive = true });
        //    }
        //    ViewData["Breadcrumbs"] = breadcrumbs;

        //}

        private void AddBreadcrumbs(int? id = 0, string? fromView = "", string? previousView = "Index")
        {
            var breadcrumbs = new List<Breadcrumb>();

            // Determina de dónde viene el usuario
            if (previousView == "ByState")
            {
                breadcrumbs.Add(new Breadcrumb { Title = "Expedientes por Estado", Url = Url.Action("ByState", "Solicitud"), IsActive = false });
            }
            else if (previousView == "MyExpedientes")
            {
                breadcrumbs.Add(new Breadcrumb { Title = "Mis Expedientes", Url = Url.Action("MyExpedientes", "Solicitud"), IsActive = false });
            }
            else
            {
                breadcrumbs.Add(new Breadcrumb { Title = "Expedientes", Url = Url.Action("Index", "Solicitud"), IsActive = false });
            }

            // Agrega la ruta correspondiente a Edit o Info
            if (fromView == "Edit")
            {
                breadcrumbs.Add(new Breadcrumb { Title = "Editar", Url = Url.Action("Edit", "Solicitud", new { id = id }), IsActive = true });
            }
            else
            {
                breadcrumbs.Add(new Breadcrumb { Title = "Información", Url = Url.Action("Info", "Solicitud", new { id = id }), IsActive = true });
            }

            ViewData["Breadcrumbs"] = breadcrumbs;
        }
    }
}
