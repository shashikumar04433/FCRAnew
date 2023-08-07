using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Controllers
{
    [ValidateFormAccess(Common.FormDefination.Home)]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class RiskCompareController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
