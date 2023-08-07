using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.RiskCriteria)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class RiskCriteriaController : MastersCustomerController<RiskCriteriaViewModel>
    {
        public RiskCriteriaController(IMasterManagerCustomer<RiskCriteriaViewModel> manager)
            : base(manager, "Risk Criteria", "Risk Criteria", null, null)
        {
        }
        protected override void SetEditProperties(ref RiskCriteriaViewModel model)
        {
            model.CustomerId = GetUserCustomerId();
        }
    }
}
