using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.Questions)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class QuestionsController : MastersCustomerController<QuestionsViewModel>
    {
        private readonly IMasterManagerCustomer<ProductServiceViewModel> _productManager;

        public QuestionsController(IMasterManagerCustomer<QuestionsViewModel> manager, IMasterManagerCustomer<ProductServiceViewModel> productManager)
            : base(manager, "Question", "Question", new[] { "Product" }, null)
        {
            _productManager = productManager;
        }
        protected override void SetProperties(ref QuestionsViewModel model)
        {
            model.ScaleType = (Common.ScaleType)GetUserCustomerScale();
        }
        protected override void SetEditProperties(ref QuestionsViewModel model)
        {
            model.CustomerId = GetUserCustomerId();
            if (model.ScaleType == Common.ScaleType.ThreePoint)
            {
                model.Scale4Value = model.Scale5Value = null;
            }
            else if (model.ScaleType == Common.ScaleType.FourPoint)
            {
                model.Scale5Value = null;
            }
        }
        protected override async Task SetIndexDropdownViewBag()
        {
            var list = await _productManager.GetAsync(GetUserCustomerId(), null, t => t.IsActive);
            ViewBag.Products = list.GetSelectList();
        }
        protected override async Task SetDropdownViewBag(QuestionsViewModel model)
        {
            var list = await _productManager.GetAsync(GetUserCustomerId(), null, t => t.IsActive);
            ViewBag.ProductId = list.GetSelectList();
        }

        protected override async Task<bool> ValidateModel(QuestionsViewModel model)
        {
            var result = await _manager.CheckExpression(GetUserCustomerId(), t => (model.Id == 0 || t.Id != model.Id)
                     && t.Name == model.Name && t.ProductId == model.ProductId);
            if (result)
            {
                ModelState.AddModelError("Name", "Name already in use");
                return result;
            }
            return result;
        }
    }
}
