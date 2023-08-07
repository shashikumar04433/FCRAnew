using FCRA.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AdminHomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
