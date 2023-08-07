using FCRA.Models.Customers;
using FCRA.Models.Masters;
using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Reports;
using FCRA.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Numerics;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.ApprovalStatus)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class ApprovalStatusController : BaseController
    {
        private readonly IApprovalMatrixManager _approvalManager;
        public ApprovalStatusController(IApprovalMatrixManager approvalMatrixManager) { 
        _approvalManager = approvalMatrixManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetApprovalStatus(string status)
        {
            var approvalStatus = _approvalManager.GetApprovalStatus(GetUserCustomerId(), status);
            return PartialView("_ApprovalStatusPartial", approvalStatus);
        }
    }
}
