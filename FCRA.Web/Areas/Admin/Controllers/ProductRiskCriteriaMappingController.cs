using FCRA.Repository.Managers;
using FCRA.ViewModels.Mappings;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.ProductRiskCriteriaMapping)]
    [Area("Admin")]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class ProductRiskCriteriaMappingController : MastersCustomerController<RiskFactorViewModel>
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<ProductServiceViewModel> _productServiceManager;
        private readonly IMasterManagerCustomer<RiskCriteriaViewModel> _riskCriteriaManager;
        private readonly IMasterManagerCustomer<RiskSubFactorViewModel> _riskSubFactorManager;
        private readonly IProductServiceMappingManager _productServiceMappingManager;
        private readonly IProductRiskCriteriaMappingManager _riskCriteriaMappingManager;
        private readonly IMasterManagerCustomer<QuestionsViewModel> _questionManager;

        public ProductRiskCriteriaMappingController(IMasterManagerCustomer<RiskFactorViewModel> manager
            , IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager
            , IMasterManagerCustomer<ProductServiceViewModel> productServiceManager, IMasterManagerCustomer<RiskCriteriaViewModel> riskCriteriaManager
            , IMasterManagerCustomer<RiskSubFactorViewModel> riskSubFactorManager
            , IProductServiceMappingManager productServiceMappingManager, IProductRiskCriteriaMappingManager riskCriteriaMappingManager
            , IMasterManagerCustomer<QuestionsViewModel> questionManager)
            : base(manager, "Risk Criteria Mappings", "Risk Criteria Mapping")
        {
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _productServiceManager = productServiceManager;
            _riskCriteriaManager = riskCriteriaManager;
            _riskSubFactorManager = riskSubFactorManager;
            _productServiceMappingManager = productServiceMappingManager;
            _riskCriteriaMappingManager = riskCriteriaMappingManager;
            _questionManager = questionManager;
        }
        public override async Task<IActionResult> Index()
        {
            ViewData["Title"] = _masterListType;
            ViewBag.StageId = (await _stageManager.GetAsync(GetUserCustomerId())).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            return View();
        }

        public async Task<IActionResult> GetCriteriaMapping(int riskFactorId, int riskSubFactorId)
        {
            var customerId = GetUserCustomerId();
            var serviceMappings = await _productServiceMappingManager.GetAsync(customerId, new[] { "ProductService" }, t => t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId);
            var criterias = await _riskCriteriaManager.GetAsync(customerId);
            var mappings = await _riskCriteriaMappingManager.GetAsync(customerId, null, t => t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId && t.IsActive);
            var questionList = await _riskCriteriaMappingManager.GetQuestionsRiskCriteriaMappings(customerId, riskFactorId, riskSubFactorId);
            foreach (var service in serviceMappings)
            {
                foreach (var criteria in criterias)
                {
                    var hasMapping = mappings.Where(t => t.ProductId == service.ProductId && t.RiskCriteriaId == criteria.Id).Any();
                    ProductRiskCriteriaMappingViewModel item = new()
                    {
                        IsSelected = hasMapping,
                        RiskFactorId = riskFactorId,
                        RiskSubFactorId = riskSubFactorId,
                        ProductId = service.ProductId,
                        RiskCriteriaId = criteria.Id
                    };
                    if (hasMapping)
                    {
                        var criteriaQuestions = questionList.Where(t => t.RiskFactorId == item.RiskFactorId && t.RiskSubFactorId == item.RiskSubFactorId
                        && t.ProductId == item.ProductId && t.RiskCriteriaId == item.RiskCriteriaId).ToList();
                        if (criteriaQuestions.Any())
                            item.QuestionIds = string.Join(',', criteriaQuestions.Select(t => t.QuestionId).ToArray());
                    }
                    service.RiskCriteriaMappings.Add(item);
                }
            }
            ViewBag.RiskCriterias = criterias;
            return PartialView("_CriteriaMappingPartial", serviceMappings);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCriteriaMapping(List<ProductRiskCriteriaMappingViewModel> mappings, int riskFactorId, int riskSubFactorId)
        {
            var result = await _riskCriteriaMappingManager.UpdateProductRiskCriteriaMappings(GetUserCustomerId(), mappings, riskFactorId, riskSubFactorId, GetUserId());
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetProductQuestions(int productId, string? excludedQuestions)
        {
            List<int> ids = new();
            if (!string.IsNullOrWhiteSpace(excludedQuestions))
            {
                ids = excludedQuestions.Split(',').Select(t => Convert.ToInt32(t)).ToList();
            }
            var model = await _questionManager.GetAsync(GetUserCustomerId(), null, t => t.IsActive && t.ProductId == productId);
            var filteredModel = model.Where(t => !ids.Any(q => q == t.Id)).ToList();
            return PartialView("_QuestionListPartial", filteredModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetProductQuestionsAdded(int productId, string? qIds)
        {
            List<int> ids = new();
            if (!string.IsNullOrWhiteSpace(qIds))
            {
                ids = qIds.Split(',').Select(t => Convert.ToInt32(t)).ToList();
            }
            var model = await _questionManager.GetAsync(GetUserCustomerId(), null, t => t.IsActive && t.ProductId == productId);
            var filteredModel = model.Where(t => ids.Any(q => q == t.Id)).ToList();
            return PartialView("_QuestionListAddedPartial", filteredModel);
        }
    }
}
