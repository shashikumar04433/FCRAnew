using FCRA.Models.Masters;
using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.BusinessSegment)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class BusinessSegmentController : MastersCustomerController<BusinessSegmentViewModel>
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<GeographicPresenceViewModel> _geographicPresenceManager;
        private readonly IMasterManagerCustomer<CustomerSegmentViewModel> _customerSegmentManager;

        public BusinessSegmentController(IMasterManagerCustomer<BusinessSegmentViewModel> manager, IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager, IMasterManagerCustomer<GeographicPresenceViewModel> geographicPresenceManager
            , IMasterManagerCustomer<CustomerSegmentViewModel> customerSegmentManager)
            : base(manager, "Sub Unit", "Sub Unit", new[] { "CustomerSegment", "CustomerSegment.GeographicPresence", "CustomerSegment.GeographicPresence.RiskType", "CustomerSegment.GeographicPresence.RiskType.Stage", "CustomerSegment.GeographicPresence.Country" }, new[] { "CustomerSegment.GeographicPresence", "CustomerSegment.GeographicPresence.RiskType", "CustomerSegment.GeographicPresence.RiskType.Stage" })
        {
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _geographicPresenceManager = geographicPresenceManager;
            _customerSegmentManager = customerSegmentManager;
        }

        protected override async Task<bool> ValidateModel(BusinessSegmentViewModel model)
        {
            var result = await _manager.CheckExpression(GetUserCustomerId(), t => (model.Id == 0 || t.Id != model.Id)
                     && t.Name == model.Name && t.CustomerSegmentId == model.CustomerSegmentId);
            if (result)
                ModelState.AddModelError("Name", "Name already in use");

            return result;
        }
        protected override void SetProperties(ref BusinessSegmentViewModel model)
        {
            model.GeographicPresenceId = model.CustomerSegment?.GeographicPresenceId;
            model.RiskTypeId = model.CustomerSegment?.GeographicPresence?.RiskTypeId;
            model.StageId = model.CustomerSegment?.GeographicPresence?.RiskType?.StageId;
            model.ScaleType = model.CustomerSegment?.GeographicPresence?.RiskType?.Stage?.ScaleType ?? Common.ScaleType.ThreePoint;
        }
        protected override void SetEditProperties(ref BusinessSegmentViewModel model)
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
        protected override async Task SetDropdownViewBag(BusinessSegmentViewModel model)
        {
            ViewBag.StageId = (await _stageManager.GetAsync(GetUserCustomerId(), null, t => !t.ExcludeChildCategory)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            if (model != null && model.StageId > 0)
            {
                ViewBag.RiskTypeId = (await _riskTypeManager.GetAsync(GetUserCustomerId(), null, t => t.StageId == model.StageId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            }
            if (model != null && model.RiskTypeId > 0)
            {
                ViewBag.GeographicPresenceId = (await _geographicPresenceManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.RiskTypeId == model.RiskTypeId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList(model.GeographicPresenceId, "Id", "CountryName");
            }
            if (model != null && model.GeographicPresenceId > 0)
            {
                ViewBag.CustomerSegmentId = (await _customerSegmentManager.GetAsync(GetUserCustomerId(), null, t => t.GeographicPresenceId == model.GeographicPresenceId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList(model.CustomerSegmentId);
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
            var list = (await _geographicPresenceManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.RiskTypeId == riskTypeId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList(null, "Id", "CountryName");
            ViewBag.ExcludeDefault = true;
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", list);
        }

        public async Task<IActionResult> GetCustomerSegmentOptions(int gId)
        {
            var list = (await _customerSegmentManager.GetAsync(GetUserCustomerId(), null, t => t.GeographicPresenceId == gId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            ViewBag.ExcludeDefault = true;
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", list);
        }
    }
}
