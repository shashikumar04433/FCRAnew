using FCRA.Repository.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public ActionResult UserNotificationGet() {
        return View("_NotificationPartial");
        }
    }
}
