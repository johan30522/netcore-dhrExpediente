using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    public class FlowWfController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
