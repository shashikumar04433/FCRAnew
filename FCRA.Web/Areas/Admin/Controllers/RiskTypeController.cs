using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.RiskType)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class RiskTypeController : MastersCustomerController<RiskTypeViewModel>
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;

        public RiskTypeController(IMasterManagerCustomer<RiskTypeViewModel> manager, IMasterManagerCustomer<StageViewModel> stageManager)
            : base(manager, "Risk Type", "Risk Type", new[] { "Stage" }, new[] { "Stage" })
        {
            _stageManager = stageManager;
        }
        protected override void SetProperties(ref RiskTypeViewModel model)
        {
            model.ScaleType = (Common.ScaleType)GetUserCustomerScale();
        }
        protected override async Task<bool> ValidateModel(RiskTypeViewModel model)
        {
            var result = await _manager.CheckExpression(GetUserCustomerId(), t => (model.Id == 0 || t.Id != model.Id)
                     && t.Name == model.Name && t.StageId == model.StageId);
            if (result)
                ModelState.AddModelError("Name", "Name already in use");

            return result;
        }
        protected override void SetEditProperties(ref RiskTypeViewModel model)
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
        protected override async Task SetDropdownViewBag(RiskTypeViewModel model)
        {
            ViewBag.StageId = (await _stageManager.GetAsync(GetUserCustomerId(), null, t => !t.ExcludeChildCategory)).OrderBy(t=>t.Sequence).ThenBy(t=>t.Name).GetSelectList();
        }
        
    }
}
