using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.Stage)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class StageController : MastersCustomerController<StageViewModel>
    {
        public StageController(IMasterManagerCustomer<StageViewModel> manager)
            : base(manager, "Stage", "Stage", null, null)
        {
        }
        protected override void SetProperties(ref StageViewModel model)
        {
            model.ScaleType = (Common.ScaleType)GetUserCustomerScale();
        }
        protected override void SetEditProperties(ref StageViewModel model)
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
        
    }
}
