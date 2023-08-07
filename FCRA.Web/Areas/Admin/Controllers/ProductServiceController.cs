using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.ProductService)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class ProductServiceController : MastersCustomerController<ProductServiceViewModel>
    {
        public ProductServiceController(IMasterManagerCustomer<ProductServiceViewModel> manager)
            : base(manager, "Product Service", "Product Service", null, null)
        {
        }
        protected override void SetEditProperties(ref ProductServiceViewModel model)
        {
            model.CustomerId = GetUserCustomerId();
        }
    }
}
