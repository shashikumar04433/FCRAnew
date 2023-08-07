using FCRA.Repository.Managers;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Customers;
using FCRA.ViewModels.Defaults;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.Customer)]
    [Area("Admin")]
    public class CustomerConfigurationController : Controller
    {
        private readonly IAccountManager _accountManager;
        public CustomerConfigurationController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetCustomerConfiguration()
        {
            var model = await _accountManager.GetCustomerConfiguration();
            return PartialView("_CustomerConfigurationPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomerConfiguration(List<CustomerConfigurationViewModel> model)
        {
            var result = await _accountManager.UpdateCustomerConfiguration(model);
            if (result == 1)
                return Json(new { status = true, message = "Request processed successfully" });

            return Json(new { status = false, message = "Somthing went wrong, please contact support!" });
        }
    }
}
