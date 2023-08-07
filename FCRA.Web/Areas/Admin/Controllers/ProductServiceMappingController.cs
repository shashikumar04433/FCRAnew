using FCRA.Common;
using FCRA.Repository.Managers;
using FCRA.ViewModels.Mappings;
using FCRA.ViewModels.Masters;
using FCRA.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.RiskFactorProductServiceMapping)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class ProductServiceMappingController : MastersCustomerController<RiskFactorViewModel>
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskSubFactorViewModel> _riskSubFactorManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IProductServiceMappingManager _productServiceMappingManager;
        private readonly IMasterManagerCustomer<ProductServiceViewModel> _productServiceManager;

        public ProductServiceMappingController(IMasterManagerCustomer<RiskFactorViewModel> manager
            , IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskSubFactorViewModel> riskSubFactorManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager
            , IProductServiceMappingManager productServiceMappingManager, IMasterManagerCustomer<ProductServiceViewModel> productServiceManager)
            : base(manager, "Product Service Mapping", "Product Service Mapping")
        {
            _stageManager = stageManager;
            _riskSubFactorManager = riskSubFactorManager;
            _riskTypeManager = riskTypeManager;
            _productServiceMappingManager = productServiceMappingManager;
            _productServiceManager = productServiceManager;
        }

        public override async Task<IActionResult> Index()
        {
            ViewData["Title"] = _masterListType;
            ViewBag.ScaleType = (ScaleType)GetUserCustomerScale();
            ViewBag.StageId = (await _stageManager.GetAsync(GetUserCustomerId())).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            return View();
        }

        public async Task<IActionResult> GetProductMapping(int riskFactorId, int riskSubFactorId)
        {
            ViewBag.ScaleType = (ScaleType)GetUserCustomerScale();
            var customerId = GetUserCustomerId();
            var products = await _productServiceManager.GetAsync(customerId);
            var mappings = await _productServiceMappingManager.GetAsync(customerId, null, t => t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId);
            List<RiskFactorProductServiceMappingViewModel> list = new();
            foreach (var item in products)
            {
                var mapping = mappings.Where(t => t.ProductId == item.Id).FirstOrDefault();
                var hasMapping = mapping != null && mapping.IsActive;
                if (mapping != null)
                {
                    list.Add(new()
                    {
                        IsSelected = hasMapping,
                        ProductId = item.Id,
                        RiskFactorId = riskFactorId,
                        RiskSubFactorId = riskSubFactorId,
                        ProductServiceName = item.Name,
                        ScaleRange2 = mapping.ScaleRange2,
                        ScaleRange3 = mapping.ScaleRange3,
                        ScaleRange4 = mapping.ScaleRange4,
                        ScaleRange5 = mapping.ScaleRange5
                    });
                }
                else
                {
                    list.Add(new()
                    {
                        IsSelected = hasMapping,
                        ProductId = item.Id,
                        RiskFactorId = riskFactorId,
                        RiskSubFactorId = riskSubFactorId,
                        ProductServiceName = item.Name
                    });
                }
            }

            return PartialView("_ProductMappingPartial", list);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductMapping(List<RiskFactorProductServiceMappingViewModel> mappings, int riskFactorId, int riskSubFactorId)
        {
            var scaleType = (ScaleType)GetUserCustomerScale();
            var result = await _productServiceMappingManager.UpdateProductServiceMappings(GetUserCustomerId(), mappings, riskFactorId, riskSubFactorId, GetUserId());
            return Json(result);
        }
    }
}
