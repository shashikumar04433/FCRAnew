using FCRA.Repository.Managers;
using FCRA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Controllers
{
    public class ExitRemarksController : BaseController
    {
        private readonly IModelManagerCustomer<ExitRemarksViewModel> _manager;

        public ExitRemarksController(IModelManagerCustomer<ExitRemarksViewModel> manager)
        {
            _manager = manager;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Audit Log";
            var list = await _manager.GetAsync(GetUserCustomerId());
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string remarks)
        {
            if (string.IsNullOrEmpty(remarks))
                return Json(-1);
            var customerId = GetUserCustomerId();
            ExitRemarksViewModel model = new()
            {
                CustomerId = customerId,
                Remarks = remarks
            };
            var result = await _manager.AddUpdateAsync(customerId, model, GetUserId());
            if (result)
                return Json(1);
            return Json(0);
        }
    }
}
