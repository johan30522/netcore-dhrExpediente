using AppExpedienteDHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Diagnostics;

namespace AppExpedienteDHR.Areas.Client.Controllers
{
    [Area("Client")]
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string? errorMessage = null, string? errorId = null, string? details = null, string? code = null)
        {
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = errorMessage ?? "Ha ocurrido un error inesperado.",
                ErrorId = errorId,
                TechnicalDetails = details, // Detalles técnicos del error
                ExceptionCode = code // Código de la excepción
            };

            return View(errorModel);
        }
    }
}
