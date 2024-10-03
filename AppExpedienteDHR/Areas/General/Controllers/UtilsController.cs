using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.General.Controllers
{
    [Authorize]
    [Area("General")]
    [Route("General/[controller]/[action]")]
    public class UtilsController : Controller
    {
        [HttpPost]
        public IActionResult SetPreviousView([FromBody] string previousView)
        {
            if (previousView == null) {
                return Ok();
            }
            HttpContext.Session.SetString("PreviousView", previousView);
            return Ok();
        }
    }
}
