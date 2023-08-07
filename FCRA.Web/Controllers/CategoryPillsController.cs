using FCRA.Models.Account;
using FCRA.Models.Masters;
using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Controllers
{
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class CategoryPillsController : BaseController
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<GeographicPresenceViewModel> _geographicPresenceManager;
        private readonly IMasterManagerCustomer<CustomerSegmentViewModel> _customerSegmentManager;
        private readonly IMasterManagerCustomer<BusinessSegmentViewModel> _businessSegmentManager;
        private readonly IReportManager _reportManager;
        private readonly IApprovalMatrixManager _approvalMatrixManager;

        public CategoryPillsController(IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager
            , IMasterManagerCustomer<GeographicPresenceViewModel> geographicPresenceManager
            , IMasterManagerCustomer<CustomerSegmentViewModel> customerSegmentManager
            , IMasterManagerCustomer<BusinessSegmentViewModel> businessSegmentManager
            , IReportManager reportManager, IApprovalMatrixManager approvalMatrixManager)
        {
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _geographicPresenceManager = geographicPresenceManager;
            _customerSegmentManager = customerSegmentManager;
            _businessSegmentManager = businessSegmentManager;
            _reportManager = reportManager;
            _approvalMatrixManager = approvalMatrixManager;
        }

        [HttpPost]
        public async Task<IActionResult> GetRiskTypeCategoryList(string xid, bool hasProgress = false)
        {
            if (!Int32.TryParse(xid.Decrypt(), out var iId))
                return NotFound();
            ViewBag.HasProgress = hasProgress;
            if (hasProgress)
            {
                ViewBag.RegisterCompletion = await _reportManager.RiskRegisterCompletionGet(GetUserCustomerId(), iId, 2);
            }
            var list = (await _riskTypeManager.GetAsync(GetUserCustomerId(), null, t => t.IsActive && t.StageId == iId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name);

            var userType = GetUserType();
            if (userType == 4 || userType == 5)
            {
                var approvalMatrix = await _approvalMatrixManager.GetApprovalMatrixAccess(GetUserCustomerId(), GetUserId());
                var RiskTypeAccessed = list.Select(s1 => s1?.Id).ToList().Intersect(approvalMatrix.Select(s2 => s2.RiskTypeId).ToList()).ToList();
                if (list != null)
                    list = list.Where(t => RiskTypeAccessed.Contains(t.Id)).OrderBy(t => t.Sequence);

            }
            return PartialView("_RiskTypeCategoryListPartial", list);
        }

        [HttpPost]
        public async Task<IActionResult> GetGeographicsPresenceCategoryList(string xid, bool hasProgress = false)
        {
            if (!Int32.TryParse(xid.Decrypt(), out var iId))
                return NotFound();
            ViewBag.HasProgress = hasProgress;
            if (hasProgress)
            {
                ViewBag.RegisterCompletion = await _reportManager.RiskRegisterCompletionGet(GetUserCustomerId(), iId, 3);
            }
            var list = (await _geographicPresenceManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.IsActive && t.RiskTypeId == iId)).OrderBy(t => t.Sequence).ThenBy(t => t.CountryName);
            var userType = GetUserType();
            if (userType == 4 || userType == 5)
            {
                var approvalMatrix = await _approvalMatrixManager.GetApprovalMatrixAccess(GetUserCustomerId(), GetUserId());
                var GeoPresenceAccessed = list.Select(s1 => s1?.Id).ToList().Intersect(approvalMatrix.Select(s2 => s2.GeographicPresenceId).ToList()).ToList();
                if (list != null)
                    list = list.Where(t => GeoPresenceAccessed.Contains(t.Id)).OrderBy(t => t.Sequence);

            }
            return PartialView("_GeographicsPresenceListPartial", list);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerSegmentCategoryList(string xid, bool hasProgress = false)
        {
            if (!Int32.TryParse(xid.Decrypt(), out var iId))
                return NotFound();
            ViewBag.HasProgress = hasProgress;
            if (hasProgress)
            {
                ViewBag.RegisterCompletion = await _reportManager.RiskRegisterCompletionGet(GetUserCustomerId(), iId, 4);
            }
            var list = (await _customerSegmentManager.GetAsync(GetUserCustomerId(), null, t => t.IsActive && t.GeographicPresenceId == iId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            var userType = GetUserType();
            if (userType == 4 || userType == 5)
            {
                var approvalMatrix = await _approvalMatrixManager.GetApprovalMatrixAccess(GetUserCustomerId(), GetUserId());
                var CustomerSegAccessed = list.Select(s1 => s1?.Id).ToList().Intersect(approvalMatrix.Select(s2 => s2.CustomerSegmentId).ToList()).ToList();
                if (list != null)
                    list = list.Where(t => CustomerSegAccessed.Contains(t.Id)).OrderBy(t => t.Sequence);

            }
            return PartialView("_CustomerSegmentCategoryListPartial", list);
        }

        [HttpPost]
        public async Task<IActionResult> GetBusinessSegmentCategoryList(string xid, bool hasProgress = false)
        {
            if (!Int32.TryParse(xid.Decrypt(), out var iId))
                return NotFound();
            ViewBag.HasProgress = hasProgress;
            if (hasProgress)
            {
                ViewBag.RegisterCompletion = await _reportManager.RiskRegisterCompletionGet(GetUserCustomerId(), iId, 5);
            }
            var list = (await _businessSegmentManager.GetAsync(GetUserCustomerId(), null, t => t.IsActive && t.CustomerSegmentId == iId)).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            var userType = GetUserType();
            if (userType == 4 || userType == 5)
            {
                var approvalMatrix = await _approvalMatrixManager.GetApprovalMatrixAccess(GetUserCustomerId(), GetUserId());
                var BusinessSegAccessed = list.Select(s1 => s1?.Id).ToList().Intersect(approvalMatrix.Select(s2 => s2.BusinessSegmentId).ToList()).ToList();
                if (list != null)
                    list = list.Where(t => BusinessSegAccessed.Contains(t.Id)).OrderBy(t => t.Sequence);

            }
            return PartialView("_BusinessSegmentListPartial", list);
        }

    }
}
