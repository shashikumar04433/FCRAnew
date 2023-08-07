using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.CustomerSegment)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class CustomerSegmentController : MastersCustomerController<CustomerSegmentViewModel>
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<GeographicPresenceViewModel> _geographicPresenceManager;

        public CustomerSegmentController(IMasterManagerCustomer<CustomerSegmentViewModel> manager, IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager, IMasterManagerCustomer<GeographicPresenceViewModel> geographicPresenceManager)
            : base(manager, "Business Segment", "Business Segment", new[] { "GeographicPresence", "GeographicPresence.RiskType", "GeographicPresence.RiskType.Stage", "GeographicPresence.Country" }, new[] { "GeographicPresence", "GeographicPresence.RiskType", "GeographicPresence.RiskType.Stage" })
        {
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _geographicPresenceManager = geographicPresenceManager;
        }

        protected override async Task<bool> ValidateModel(CustomerSegmentViewModel model)
        {
            var result = await _manager.CheckExpression(GetUserCustomerId(), t => (model.Id == 0 || t.Id != model.Id)
                     && t.Name == model.Name && t.GeographicPresenceId == model.GeographicPresenceId);
            if (result)
                ModelState.AddModelError("Name", "Name already in use");

            return result;
        }
        protected override void SetProperties(ref CustomerSegmentViewModel model)
        {
            model.RiskTypeId = model.GeographicPresence?.RiskTypeId;
            model.StageId = model.GeographicPresence?.RiskType?.StageId;
            model.ScaleType = model.GeographicPresence?.RiskType?.Stage?.ScaleType ?? Common.ScaleType.ThreePoint;
        }
        protected override void SetEditProperties(ref CustomerSegmentViewModel model)
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
        protected override async Task SetDropdownViewBag(CustomerSegmentViewModel model)
        {
            ViewBag.StageId = (await _stageManager.GetAsync(GetUserCustomerId(), null, t => !t.ExcludeChildCategory)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            if (model != null && model.StageId > 0)
            {
                ViewBag.RiskTypeId = (await _riskTypeManager.GetAsync(GetUserCustomerId(), null, t => t.StageId == model.StageId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            }
            if (model != null && model.RiskTypeId > 0)
            {
                ViewBag.GeographicPresenceId = (await _geographicPresenceManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.RiskTypeId == model.RiskTypeId)).OrderBy(t => t.Sequence).ThenBy(t => t.CountryName).GetSelectList(null, "Id", "CountryName");
            }
        }

        public async Task<IActionResult> GetRiskTypeOptions(int stageId)
        {
            var list = (await _riskTypeManager.GetAsync(GetUserCustomerId(), null, t => t.StageId == stageId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            ViewBag.ExcludeDefault = true;
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", list);
        }

        public async Task<IActionResult> GetGeographicPresenceOptions(int riskTypeId)
        {
            var list = (await _geographicPresenceManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.RiskTypeId == riskTypeId)).OrderBy(t => t.Sequence).ThenBy(t => t.CountryName).GetSelectList(null, "Id", "CountryName");
            ViewBag.ExcludeDefault = true;
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", list);
        }
    }
}
