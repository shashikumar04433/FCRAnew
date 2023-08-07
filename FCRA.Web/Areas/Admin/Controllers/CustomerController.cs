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
    public class CustomerController : MastersController<CustomerViewModel>
    {
        private readonly IIdModelManager<FormViewModel> _formManager;
        private readonly IIdModelManager<DefaultScaleViewModel> _defaultScaleManager;
        private readonly IMasterManager<CountryViewModel> _countryManager;

        public CustomerController(IMasterManager<CustomerViewModel> manager, IIdModelManager<FormViewModel> formManager
            , IIdModelManager<DefaultScaleViewModel> defaultScaleManager, IMasterManager<CountryViewModel> countryManager)
            : base(manager, "Customer", "Customer", null, new string[] { "Locations", "Scales", "Scales.DefaultScale", "Forms" })
        {
            _formManager = formManager;
            _defaultScaleManager = defaultScaleManager;
            _countryManager = countryManager;
        }

        protected override void SetProperties(ref CustomerViewModel model)
        {
            var formList = _formManager.GetAsync().Result;
            var difaultScaleList = _defaultScaleManager.GetAsync().Result;
            foreach (var item in formList)
            {
                var existingItem = model.Forms.FirstOrDefault(t => t.FormId == item.Id);
                if (existingItem != null)
                {
                    existingItem.DefaultFormName = item.Name;
                    continue;
                }
                model.Forms.Add(new()
                {
                    FormId = item.Id,
                    CustomerId = model.Id,
                    DefaultFormName = item.Name,
                    FormName = item.Name
                });
            }
            foreach (var item in difaultScaleList)
            {
                var existingItem = model.Scales.FirstOrDefault(t => t.ScaleId == item.Id);
                if (existingItem == null)
                {
                    model.Scales.Add(new()
                    {
                        CustomerId = model.Id,
                        ScaleId = item.Id,
                        Name = item.Name,
                        DefaultScale = item
                    });
                }
                else
                {
                    existingItem.DefaultScale = item;
                }
            }
        }

        //protected override void SetEditProperties(ref CustomerViewModel model)
        //{
        //    SetProperties(ref model);
        //}

        protected override async Task SetDropdownViewBag(CustomerViewModel model)
        {
            if (model.Id > 0 && (model.Locations.Any()))
                ViewBag.CountryId = (await _countryManager.GetAsync()).GetSelectList();
        }
        protected async Task SetDropdownViewBagTemplate()
        {
            ViewBag.CountryId = (await _countryManager.GetAsync()).GetSelectList();
        }
        public async Task<IActionResult> GetLocationTemplate(int customerId)
        {
            CustomerViewModel model = new()
            {
                Locations = new() { new() { CustomerId = customerId } }
            };
            await SetDropdownViewBagTemplate();
            return PartialView("_LocationDetailsPartial", model);
        }
        public async Task<IActionResult> GetCountryTemplate(int customerId)
        {
            CustomerViewModel model = new();
            //{
            //    Countries = new() { new() { CustomerId = customerId } }
            //};
            await SetDropdownViewBagTemplate();
            return PartialView("_CountryDetailsPartial", model);
        }
    }
}
