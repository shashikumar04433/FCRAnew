using FCRA.Models.Masters;
using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.GeographicPresence)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class GeographicPresenceController : MastersCustomerController<GeographicPresenceViewModel>
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManager<CountryViewModel> _countryManager;

        public GeographicPresenceController(IMasterManagerCustomer<GeographicPresenceViewModel> manager, IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager, IMasterManager<CountryViewModel> countryManager)
            : base(manager, "Geographic Presence", "Geographic Presence", new[] { "Country", "RiskType", "RiskType.Stage" }, new[] { "RiskType", "RiskType.Stage" })
        {
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _countryManager = countryManager;
        }
        public override async Task<IActionResult> Index()
        {
            ViewData["Title"] = _masterListType;
            var list = await _manager.GetWithoutOrderAsync(GetUserCustomerId(), _masterGetInclude);
            await SetIndexDropdownViewBag();
            return View(list);
        }
        protected override async Task<bool> ValidateModel(GeographicPresenceViewModel model)
        {
            var result = await _manager.CheckExpression(GetUserCustomerId(), t => (model.Id == 0 || t.Id != model.Id)
                     && t.CountryId == model.CountryId && t.RiskTypeId == model.RiskTypeId);
            if (result)
                ModelState.AddModelError("Name", "Country already in use");

            return result;
        }

        protected override void SetProperties(ref GeographicPresenceViewModel model)
        {
            model.StageId = model.RiskType?.StageId;
            model.ScaleType = (Common.ScaleType)GetUserCustomerScale();
        }
        protected override void SetEditProperties(ref GeographicPresenceViewModel model)
        {
            model.CustomerId = GetUserCustomerId();
            if (model.ScaleType == Common.ScaleType.ThreePoint)
            {
                model.ScaleRange4 = model.ScaleRange5 = null;
            }
            else if (model.ScaleType == Common.ScaleType.FourPoint)
            {
                model.ScaleRange5 = null;
            }
        }
        protected override async Task SetDropdownViewBag(GeographicPresenceViewModel model)
        {
            ViewBag.StageId = (await _stageManager.GetAsync(GetUserCustomerId(), null, t => !t.ExcludeChildCategory)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            ViewBag.CountryId = (await _countryManager.GetAsync()).GetSelectList();
            if (model != null && model.StageId > 0)
            {
                ViewBag.RiskTypeId = (await _riskTypeManager.GetAsync(GetUserCustomerId(), null, t => t.StageId == model.StageId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            }
        }

        public async Task<IActionResult> GetRiskTypeOptions(int stageId)
        {
            var list = (await _riskTypeManager.GetAsync(GetUserCustomerId(), null, t => t.StageId == stageId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            ViewBag.ExcludeDefault = true;
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", list);
        }
    }
}
