using FCRA.Repository.Managers;
using FCRA.ViewModels;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.PreDefinedRiskParameter)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class PreDefinedRiskParameterController : MastersCustomerController<PreDefinedRiskParameterViewModel>
    {
        public PreDefinedRiskParameterController(IMasterManagerCustomer<PreDefinedRiskParameterViewModel> manager) 
            :base(manager, "Pre Defined Risk Parameter", "Pre Defined Risk Parameter", null, null)
        {
        }
        protected override void SetEditProperties(ref PreDefinedRiskParameterViewModel model)
        {
            model.CustomerId = GetUserCustomerId();
        }
    }
}
