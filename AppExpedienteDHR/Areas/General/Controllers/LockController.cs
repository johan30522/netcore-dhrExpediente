using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppExpedienteDHR.Areas.General.Controllers
{
    [Authorize]
    [Area("General")]
    [Route("General/[controller]/[action]")]
    public class LockController : Controller
    {
        private readonly ILockRecordService _lockRecordService;
        private readonly IUserService _userService;

        public LockController(ILockRecordService lockRecordService, IUserService userService)
        {
            _lockRecordService = lockRecordService;
            _userService = userService;
        }

        /// <summary>
        /// Mantiene vivo el bloqueo de un registro (heartbeat).
        /// </summary>
        /// <param name="lockId">ID del registro bloqueado.</param>
        /// <returns>Json indicando si el bloqueo se mantuvo.</returns>
        [HttpPost]
        public async Task<IActionResult> KeepAlive([FromQuery] int lockId)
        {
            bool success = await _lockRecordService.KeepAliveLock(lockId);
            if (success)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "No se pudo mantener el bloqueo." });
            }
        }
        /// <summary>
        /// Desbloquea un registro cuando el usuario sale de la página.
        /// </summary>
        /// <param name="lockId">ID del registro bloqueado.</param>
        /// <returns>Json indicando si el bloqueo se liberó.</returns>
        [HttpPost]
        public async Task<IActionResult> Unlock([FromQuery] int lockId)
        {
            UserViewModel user = await _userService.GetCurrentUser();
            await _lockRecordService.UnlockById(lockId);
            return Json(new { success = true });
        }
    }

}
