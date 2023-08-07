using FCRA.Common;
using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.RiskFactor)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class RiskFactorController : MastersCustomerController<RiskFactorViewModel>
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<GeographicPresenceViewModel> _geographicPresenceManager;
        private readonly IMasterManagerCustomer<CustomerSegmentViewModel> _customerSegmentManager;
        private readonly IMasterManagerCustomer<BusinessSegmentViewModel> _businessSegmentanager;

        public RiskFactorController(IMasterManagerCustomer<RiskFactorViewModel> manager, IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager, IMasterManagerCustomer<GeographicPresenceViewModel> geographicPresenceManager
            , IMasterManagerCustomer<CustomerSegmentViewModel> customerSegmentManager, IMasterManagerCustomer<BusinessSegmentViewModel> businessSegmentanager)
            : base(manager, "Risk Factor", "Risk Factor", new[] { "BusinessSegment", "CustomerSegment", "GeographicPresence", "GeographicPresence.Country", "RiskType", "Stage" }, new[] { "BusinessSegment", "CustomerSegment", "GeographicPresence", "RiskType", "Stage" })
        {
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _geographicPresenceManager = geographicPresenceManager;
            _customerSegmentManager = customerSegmentManager;
            _businessSegmentanager = businessSegmentanager;
        }

        protected override void SetProperties(ref RiskFactorViewModel model)
        {
            model.ScaleType = (ScaleType)GetUserCustomerScale();
        }
        protected override void SetEditProperties(ref RiskFactorViewModel model)
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
        protected override async Task SetDropdownViewBag(RiskFactorViewModel? model)
        {
            var customerId = GetUserCustomerId();
            ViewBag.StageId = (await _stageManager.GetAsync(customerId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            if (model != null && model.StageId > 0)
            {
                ViewBag.RiskTypeId = (await _riskTypeManager.GetAsync(customerId, null, t => t.StageId == model.StageId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            }
            if (model != null && model.RiskTypeId > 0)
            {
                ViewBag.GeographicPresenceId = (await _geographicPresenceManager.GetWithoutOrderAsync(customerId, new[] { "Country" }, t => t.RiskTypeId == model.RiskTypeId)).OrderBy(t => t.Sequence).ThenBy(t => t.CountryName).ToList();
            }
            if (model != null && model.GeographicPresenceId > 0)
            {
                ViewBag.CustomerSegmentId = (await _customerSegmentManager.GetAsync(customerId, null, t => t.GeographicPresenceId == model.GeographicPresenceId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            }
            if (model != null && model.CustomerSegmentId > 0)
            {
                ViewBag.BusinessSegmentId = (await _businessSegmentanager.GetAsync(customerId, null, t => t.CustomerSegmentId == model.CustomerSegmentId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList(model.BusinessSegmentId);
            }
        }
        protected override async Task<bool> ValidateModel(RiskFactorViewModel model)
        {
            var result = await _manager.CheckExpression(GetUserCustomerId(), t => (model.Id == 0 || t.Id != model.Id)
                     && t.Name == model.Name
                    && t.StageId == model.StageId && ((!model.RiskTypeId.HasValue && !t.RiskTypeId.HasValue) || (t.RiskTypeId == model.RiskTypeId))
                    && ((!model.GeographicPresenceId.HasValue && !t.GeographicPresenceId.HasValue) || (t.GeographicPresenceId == model.GeographicPresenceId))
                    && ((!model.CustomerSegmentId.HasValue && !t.CustomerSegmentId.HasValue) || (t.CustomerSegmentId == model.CustomerSegmentId))
                    && ((!model.BusinessSegmentId.HasValue && !t.BusinessSegmentId.HasValue) || (t.BusinessSegmentId == model.BusinessSegmentId)));
            if (result)
                ModelState.AddModelError("Name", "Name already in use");

            var list = await _manager.GetAsync(GetUserCustomerId(), null, t => t.Id != model.Id
                    && t.StageId == model.StageId && ((!model.RiskTypeId.HasValue && !t.RiskTypeId.HasValue) || (t.RiskTypeId == model.RiskTypeId))
                    && ((!model.GeographicPresenceId.HasValue && !t.GeographicPresenceId.HasValue) || (t.GeographicPresenceId == model.GeographicPresenceId))
                    && ((!model.CustomerSegmentId.HasValue && !t.CustomerSegmentId.HasValue) || (t.CustomerSegmentId == model.CustomerSegmentId))
                    && ((!model.BusinessSegmentId.HasValue && !t.BusinessSegmentId.HasValue) || (t.BusinessSegmentId == model.BusinessSegmentId)));
            if (model.WeightPercentage > 0)
            {
                var totalWeightage = list.Sum(t => t.WeightPercentage);
                totalWeightage += model.WeightPercentage;
                if (totalWeightage > 100)
                {
                    ModelState.AddModelError("WeightPercentage", "Total risk weightage cannot exceed 100%");
                    result = true;
                }
            }
            return result;
        }

        public async Task<IActionResult> GetRiskTypeOptions(int stageId)
        {
            var list = (await _riskTypeManager.GetAsync(GetUserCustomerId(), null, t => t.StageId == stageId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            //ViewBag.ExcludeDefault = true;
            List<SelectListItem> selectList = list.Select(t => new SelectListItem { Value = Convert.ToString(t.Id), Text = t.Name, Disabled = t.ExcludeChildCategory }).ToList();
            return PartialView("~/Views/Shared/_OptionsPartialEx.cshtml", selectList);
        }

        public async Task<IActionResult> GetGeographicPresenceOptions(int riskTypeId)
        {
            var list = (await _geographicPresenceManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.RiskTypeId == riskTypeId)).OrderBy(t => t.Sequence).ThenBy(t => t.CountryName);
            //ViewBag.ExcludeDefault = true;
            List<SelectListItem> selectList = list.Select(t => new SelectListItem { Value = Convert.ToString(t.Id), Text = t.CountryName, Disabled = t.ExcludeChildCategory }).ToList();
            return PartialView("~/Views/Shared/_OptionsPartialEx.cshtml", selectList);
        }

        public async Task<IActionResult> GetCustomerSegmentOptions(int gId)
        {
            var list = (await _customerSegmentManager.GetAsync(GetUserCustomerId(), null, t => t.GeographicPresenceId == gId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            //ViewBag.ExcludeDefault = true;
            List<SelectListItem> selectList = list.Select(t => new SelectListItem { Value = Convert.ToString(t.Id), Text = t.Name, Disabled = t.ExcludeChildCategory }).ToList();
            return PartialView("~/Views/Shared/_OptionsPartialEx.cshtml", selectList);
        }

        public async Task<IActionResult> GetBusinessSegmentOptions(int cId)
        {
            var list = await _businessSegmentanager.GetAsync(GetUserCustomerId(), null, t => t.CustomerSegmentId == cId);
            ViewBag.ExcludeDefault = true;
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", list.GetSelectList());
        }
    }
}
