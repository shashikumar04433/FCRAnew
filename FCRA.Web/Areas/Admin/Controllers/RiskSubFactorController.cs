using FCRA.Common;
using FCRA.Models.Masters;
using FCRA.Repository.Managers;
using FCRA.ViewModels;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Text;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.RiskSubFactor)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class RiskSubFactorController : MastersCustomerController<RiskSubFactorViewModel>
    {
        private readonly StorageSettings _storageSettings;
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<GeographicPresenceViewModel> _geographicPresenceManager;
        private readonly IMasterManagerCustomer<CustomerSegmentViewModel> _customerSegmentManager;
        private readonly IMasterManagerCustomer<BusinessSegmentViewModel> _businessSegmentanager;
        private readonly IMasterManagerCustomer<RiskFactorViewModel> _riskFactorManager;
        private readonly IMasterManagerCustomer<PreDefinedRiskParameterViewModel> _preDefinedManager;

        public RiskSubFactorController(IMasterManagerCustomer<RiskSubFactorViewModel> manager, IOptions<StorageSettings> options, IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager, IMasterManagerCustomer<GeographicPresenceViewModel> geographicPresenceManager
            , IMasterManagerCustomer<CustomerSegmentViewModel> customerSegmentManager, IMasterManagerCustomer<BusinessSegmentViewModel> businessSegmentanager
            , IMasterManagerCustomer<RiskFactorViewModel> riskFactorManager
            , IMasterManagerCustomer<PreDefinedRiskParameterViewModel> preDefinedManager)
            : base(manager, "Risk Sub Factor", "Risk Sub Factor", new[] { "RiskFactor", "RiskFactor.RiskType" , "RiskFactor.GeographicPresence"
                , "RiskFactor.GeographicPresence.Country", "RiskFactor.CustomerSegment", "RiskFactor.BusinessSegment","RiskFactor.Stage" }, new[] { "RiskFactor" })
        {
            _storageSettings = options.Value;
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _geographicPresenceManager = geographicPresenceManager;
            _customerSegmentManager = customerSegmentManager;
            _businessSegmentanager = businessSegmentanager;
            _riskFactorManager = riskFactorManager;
            _preDefinedManager = preDefinedManager;
        }

        protected override void SetProperties(ref RiskSubFactorViewModel model)
        {
            base.SetProperties(ref model);
            model.ScaleType = (ScaleType)GetUserCustomerScale();
            model.StageId = model.RiskFactor?.StageId;
            model.RiskTypeId = model.RiskFactor?.RiskTypeId;
            model.GeographicPresenceId = model.RiskFactor?.GeographicPresenceId;
            model.CustomerSegmentId = model.RiskFactor?.CustomerSegmentId;
            model.BusinessSegmentId = model.RiskFactor?.BusinessSegmentId;

            if (model != null && (model.Id == 0 || model.RiskFactor == null))
                model.IsExcludedInRisk = true;
            if (model != null && model.RiskFactor != null)
            {
                model.IsExcludedInRisk = model.RiskFactor.IsExcludedInRisk;
                model.RiskRangeParameter = model.RiskFactor.RiskRangeParameter;
            }
        }

        protected override void SetEditProperties(ref RiskSubFactorViewModel model)
        {
            model.CustomerId = GetUserCustomerId();
        }
        protected override async Task SetDropdownViewBag(RiskSubFactorViewModel? model)
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
            if (model != null && model.StageId > 0)
            {
                ViewBag.RiskFactorId = (await _riskFactorManager.GetAsync(customerId, null
                    , t => t.StageId == model.StageId && ((!model.RiskTypeId.HasValue && !t.RiskTypeId.HasValue) || (t.RiskTypeId == model.RiskTypeId))
                    && ((!model.GeographicPresenceId.HasValue && !t.GeographicPresenceId.HasValue) || (t.GeographicPresenceId == model.GeographicPresenceId))
                    && ((!model.CustomerSegmentId.HasValue && !t.CustomerSegmentId.HasValue) || (t.CustomerSegmentId == model.CustomerSegmentId))
                    && ((!model.BusinessSegmentId.HasValue && !t.BusinessSegmentId.HasValue) || (t.BusinessSegmentId == model.BusinessSegmentId)))).OrderBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            }

            var riskPreDefinedList = await _preDefinedManager.GetAsync(customerId);
            ViewBag.scaletype = GetUserCustomerScale();
            ViewBag.PreDefinedParameter1Id = riskPreDefinedList.GetSelectList(model?.PreDefinedParameter1Id);
            ViewBag.PreDefinedParameter2Id = riskPreDefinedList.GetSelectList(model?.PreDefinedParameter2Id);
            ViewBag.PreDefinedParameter3Id = riskPreDefinedList.GetSelectList(model?.PreDefinedParameter3Id);
            ViewBag.PreDefinedParameter4Id = riskPreDefinedList.GetSelectList(model?.PreDefinedParameter4Id);
            ViewBag.PreDefinedParameter5Id = riskPreDefinedList.GetSelectList(model?.PreDefinedParameter5Id);
        }

        protected override async Task<bool> ValidateModel(RiskSubFactorViewModel model)
        {
            var result = await _manager.CheckExpression(GetUserCustomerId(), t => (model.Id == 0 || t.Id != model.Id)
                     && t.Name == model.Name && t.RiskFactorId == model.RiskFactorId);
            if (result)
            {
                ModelState.AddModelError("Name", "Name already in use");
                return result;
            }
            var list = await _manager.GetAsync(GetUserCustomerId(), null, t => t.Id != model.Id && t.RiskFactorId == model.RiskFactorId);
            if (model.RiskWeightage.HasValue)
            {
                var totalWeightage = list.Where(r => r.RiskWeightage.HasValue).Sum(t => t.RiskWeightage);
                totalWeightage += model.RiskWeightage.Value;
                if (totalWeightage > 100)
                {
                    ModelState.AddModelError("RiskWeightage", "Total risk weightage cannot exceed 100%");
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
        public async Task<string> GetRiskFactorOptions(int stageId, int? riskTypeId
            , int? geoPresenceId, int? customerSegmentId, int? businessSegmentId)
        {
            var riskFactorList = (await _riskFactorManager.GetAsync(GetUserCustomerId(), null,
                t => t.StageId == stageId && ((!riskTypeId.HasValue && !t.RiskTypeId.HasValue) || (t.RiskTypeId == riskTypeId))
                && ((!geoPresenceId.HasValue && !t.GeographicPresenceId.HasValue) || (t.GeographicPresenceId == geoPresenceId))
                && ((!customerSegmentId.HasValue && !t.CustomerSegmentId.HasValue) || (t.CustomerSegmentId == customerSegmentId))
                && ((!businessSegmentId.HasValue && !t.BusinessSegmentId.HasValue) || (t.BusinessSegmentId == businessSegmentId)))).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            ViewBag.ExcludeDefault = true;
            StringBuilder sb = new("<option value=\"\">--Select--</option>");
            foreach (var item in riskFactorList) { sb.Append($"<option value=\"{item.Id}\" data-ex=\"{item.IsExcludedInRisk}\" data-param=\"{(int)item.RiskRangeParameter}\">{item.Name}</option>"); }
            return sb.ToString();
        }


    }
}
