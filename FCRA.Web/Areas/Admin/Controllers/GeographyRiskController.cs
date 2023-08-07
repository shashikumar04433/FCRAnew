using FCRA.Common;
using FCRA.Repository.Managers;
using FCRA.ViewModels.Customers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.Stage)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class GeographyRiskController : MastersCustomerController<GeographyRiskViewModel>
    {
        private readonly IMasterManager<CountryViewModel> _countryManager;

        public GeographyRiskController(IMasterManagerCustomer<GeographyRiskViewModel> manager,
            IMasterManager<CountryViewModel> countryManager)
           : base(manager, "Geography Risk", "Geography Risk", null, null)
        {
            _countryManager = countryManager;
        }
        public override async Task<IActionResult> Index()
        {
            ViewData["Title"] = _masterListType;
            var list = await _manager.GetWithoutOrderAsync(GetUserCustomerId(), _masterGetInclude);

            var countrylist =await _countryManager.GetAsync();
            foreach (var item in countrylist)
            {
                var gItem = list.FirstOrDefault(t => t.CountryId == item.Id);
                if (gItem == null)
                    list.Add(new GeographyRiskViewModel()
                    {
                        CountryId = item.Id,
                        Name = item.Name,
                        IsActive = false
                    });
                else
                    gItem.Name = item.Name;
            }
            ViewBag.ScaleType = (ScaleType)GetUserCustomerScale();
            return View(list);
        }
        protected override void SetProperties(ref GeographyRiskViewModel model)
        {
            model.CustomerId = GetUserCustomerId();
        }
        protected override void SetEditProperties(ref GeographyRiskViewModel model)
        {
            model.CustomerId = GetUserCustomerId();
        }
        [HttpPost]
        public async Task<IActionResult> IndexRange(List<GeographyRiskViewModel> model)
        {
            var result = await _manager.AddUpdateRangeAsync(GetUserCustomerId(), model.ToList(), this.GetUserId());
            if (result)
            {
                SetApplicationResult(true, $"{_masterType} {("Saved")} successfully");
                return Redirect("Index");
            }
            ModelState.AddModelError(_globalErrorField, "Something went worng, please contact support");

            return RedirectToAction("Index");
        }

    }

}
