using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using FCRA.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CommonCategoryController : BaseController
    {
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<GeographicPresenceViewModel> _geographicPresenceManager;
        private readonly IMasterManagerCustomer<CustomerSegmentViewModel> _customerSegmentManager;
        private readonly IMasterManagerCustomer<BusinessSegmentViewModel> _businessSegmentanager;
        private readonly IMasterManagerCustomer<RiskFactorViewModel> _riskFactorManager;
        private readonly IMasterManagerCustomer<RiskSubFactorViewModel> _riskSubFactorManager;

        public CommonCategoryController(IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager
            , IMasterManagerCustomer<GeographicPresenceViewModel> geographicPresenceManager
            , IMasterManagerCustomer<CustomerSegmentViewModel> customerSegmentManager
            , IMasterManagerCustomer<BusinessSegmentViewModel> businessSegmentanager
            , IMasterManagerCustomer<RiskFactorViewModel> riskFactorManager
            , IMasterManagerCustomer<RiskSubFactorViewModel> riskSubFactorManager)
        {
            _riskTypeManager = riskTypeManager;
            _geographicPresenceManager = geographicPresenceManager;
            _customerSegmentManager = customerSegmentManager;
            _businessSegmentanager = businessSegmentanager;
            _riskFactorManager = riskFactorManager;
            _riskSubFactorManager = riskSubFactorManager;
        }
        public async Task<IActionResult> GetRiskTypeOptions(int stageId)
        {
            var list = (await _riskTypeManager.GetAsync(GetUserCustomerId(), null, t => t.StageId == stageId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            List<SelectListItem> selectList = list.Select(t => new SelectListItem { Value = Convert.ToString(t.Id), Text = t.Name, Disabled = t.ExcludeChildCategory }).ToList();
            return PartialView("~/Views/Shared/_OptionsPartialEx.cshtml", selectList);
        }

        public async Task<IActionResult> GetGeographicPresenceOptions(int riskTypeId)
        {
            var list = (await _geographicPresenceManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.RiskTypeId == riskTypeId)).OrderBy(t => t.Sequence).ThenBy(t => t.CountryName);
            List<SelectListItem> selectList = list.Select(t => new SelectListItem { Value = Convert.ToString(t.Id), Text = t.CountryName, Disabled = t.ExcludeChildCategory }).ToList();
            return PartialView("~/Views/Shared/_OptionsPartialEx.cshtml", selectList);
        }

        public async Task<IActionResult> GetCustomerSegmentOptions(int gId)
        {
            var list = (await _customerSegmentManager.GetAsync(GetUserCustomerId(), null, t => t.GeographicPresenceId == gId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            List<SelectListItem> selectList = list.Select(t => new SelectListItem { Value = Convert.ToString(t.Id), Text = t.Name, Disabled = t.ExcludeChildCategory }).ToList();
            return PartialView("~/Views/Shared/_OptionsPartialEx.cshtml", selectList);
        }

        public async Task<IActionResult> GetBusinessSegmentOptions(int cId)
        {
            var list = (await _businessSegmentanager.GetAsync(GetUserCustomerId(), null, t => t.CustomerSegmentId == cId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            ViewBag.ExcludeDefault = true;
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", list);
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
        public async Task<IActionResult> GetRiskSubFactorOptions(int rId)
        {
            var list = (await _riskSubFactorManager.GetAsync(GetUserCustomerId(), null, t => t.RiskFactorId == rId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name).GetSelectList();
            ViewBag.ExcludeDefault = true;
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", list);
        }
    }
}
