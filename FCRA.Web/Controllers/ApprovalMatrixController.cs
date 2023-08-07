using FCRA.Common;
using FCRA.Models.Customers;
using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using FCRA.Web.Areas.Admin.Controllers;
using FCRA.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace FCRA.Web.Controllers
{
    [ValidateFormAccess(Common.FormDefination.ApprovalMatrix)]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class ApprovalMatrixController : BaseController
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<GeographicPresenceViewModel> _geographicPresenceManager;
        private readonly IMasterManagerCustomer<CustomerSegmentViewModel> _customerSegmentManager;
        private readonly IMasterManagerCustomer<BusinessSegmentViewModel> _businessSegmentanager;
        private readonly IApprovalMatrixManager _approvalMatrixManager;
        private readonly IRiskAssessmentManager _riskAssessmentManager;
        private readonly IAuditTrailManager _auditTrailManager;

        public ApprovalMatrixController(IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager, IMasterManagerCustomer<GeographicPresenceViewModel> geographicPresenceManager
            , IMasterManagerCustomer<CustomerSegmentViewModel> customerSegmentManager, IMasterManagerCustomer<BusinessSegmentViewModel> businessSegmentanager
            , IApprovalMatrixManager approvalMatrixManager, IRiskAssessmentManager riskAssessmentManager, IAuditTrailManager auditTrailManager)
        {
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _geographicPresenceManager = geographicPresenceManager;
            _customerSegmentManager = customerSegmentManager;
            _businessSegmentanager = businessSegmentanager;
            _approvalMatrixManager = approvalMatrixManager;
            _riskAssessmentManager = riskAssessmentManager;
            _auditTrailManager = auditTrailManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ApprovalMatrixViewModel();
            ViewBag.StageId = await _stageManager.GetAsync(GetUserCustomerId());
            ViewBag.ApproverUserList = (await _riskAssessmentManager.GetApproverList(GetUserCustomerId()));
            return View(model);
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
            var list = await _businessSegmentanager.GetAsync(GetUserCustomerId(), null, t => t.CustomerSegmentId == cId);
            ViewBag.ExcludeDefault = true;
            return PartialView("~/Views/Shared/_OptionsPartial.cshtml", list.GetSelectList());
        }
        
        public async Task<IActionResult> GetApprovalMatrix(RiskFactorViewModel model)
        {
            var matrixList = await _approvalMatrixManager.GetApprovalMatrixViewModelAsync(model);
            var userlist = (await _auditTrailManager.GetUserList());
            foreach(var matrix in matrixList)
            {
                var user = userlist.Where(x => x.Id == matrix.UserId).FirstOrDefault();
                matrix.UserName = user?.Name;
            }
            return PartialView("_MatrixDetailsPartial", matrixList);
        }
        [HttpPost]
        public async Task<IActionResult> SaveApprovalMatrix(List<ApprovalMatrixViewModel> model)
        {
            var result =  await _approvalMatrixManager.SaveApprovalMatrix(model, GetUserCustomerId(), GetUserId());
            return Json(new AppResultViewModel { Status = result, Message = result ? "Approver updated successfully" : "Something went wrong!" });
        }
    }
}
