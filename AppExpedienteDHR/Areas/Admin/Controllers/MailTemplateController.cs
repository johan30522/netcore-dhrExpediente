using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class MailTemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
