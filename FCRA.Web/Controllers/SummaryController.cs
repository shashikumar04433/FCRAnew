using FCRA.Common;
using FCRA.Models.Customers;
using FCRA.Models.Masters;
using FCRA.Repository.Managers;
using FCRA.ViewModels;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace FCRA.Web.Controllers
{
    [Authorize]
    public class SummaryController : BaseController
    {
        private readonly IReportManager _reportManager;
        private readonly IMasterManagerCustomer<RiskFactorViewModel> _riskFactorManager;
        private readonly IRiskAssessmentManager _riskAssessmentManager;

        public SummaryController(IReportManager reportManager
            , IMasterManagerCustomer<RiskFactorViewModel> riskFactorManager
            , IRiskAssessmentManager riskAssessmentManager
            )
        {
            _reportManager = reportManager;
            _riskFactorManager = riskFactorManager;
            _riskAssessmentManager = riskAssessmentManager;
        }
        public async Task<IActionResult> Index()
        {
            SummaryViewModel model = new();
            model.InherentRisksSummary = await _reportManager.GetInherentRisksSummary();
            model.ControlSummary = await _reportManager.GetRisksSummary(3);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SummaryDetails(string riskTpye, string group, string selectedriskFactorId = "", int countryId = 0, string customerSegmentName = "", bool isDrillDown = false)
        {
            ViewBag.ScaleType = GetUserCustomerScale();

            var riskTypeList = riskTpye.Decrypt().Split(',').Select(t => Convert.ToInt32(t)).ToList();
            var groupList = group.Split(',').Select(t => Convert.ToInt32(t)).ToList();
            var riskFactorList = selectedriskFactorId!.Split(',').Select(t => Convert.ToInt32(t)).ToList();
            ViewBag.Groups = groupList;
            var customerId = GetUserCustomerId();
            AssessmentPillsViewModel model = new();

            List<RiskFactorViewModel> riskFactors = (await _riskFactorManager.GetAsync(customerId
                , new[] { "Stage", "RiskType", "GeographicPresence", "GeographicPresence.Country", "CustomerSegment", "BusinessSegment" }
                , t => t.IsActive))
                .OrderBy(t => t.Stage!.Sequence).ThenBy(t => t.Stage!.Name)
                .OrderBy(t => t.RiskType!.Sequence).ThenBy(t => t.RiskType!.Name)
                .OrderBy(t => t.GeographicPresence?.Sequence).ThenBy(t => t.GeographicPresence?.CountryName)
                .OrderBy(t => t.CustomerSegment?.Sequence).ThenBy(t => t.CustomerSegment?.Name)
                .OrderBy(t => t.BusinessSegment?.Sequence).ThenBy(t => t.BusinessSegment?.Name)
                .ThenBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            riskFactors = riskFactors.Where(t => riskTypeList.Contains(t.RiskTypeId!.Value)).ToList();
            if (riskFactorList.Count > 0 && riskFactorList[0] > 0)
                riskFactors = riskFactors.Where(t => riskFactorList.Contains(t.Id)).ToList();
            List<int> riskFactorIds = new();
            if (riskFactors != null && riskFactors.Any())
            {
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
                //Factor responses
                var riskFactorResponses = await _riskAssessmentManager.GetRiskFactorResponse(customerId, riskFactorIds);
                if (riskFactorResponses != null && riskFactorResponses.Any())
                {
                    foreach (var factor in riskFactors)
                    {
                        var response = riskFactorResponses.Where(t => t.RiskFactorId == factor.Id).FirstOrDefault();
                        if (response != null)
                        {
                            factor.TotalWeightedScore = response.TotalWeightedScore;
                        }
                    }
                }

                if(groupList.Contains(6) && isDrillDown == true)
                {
                    model.RiskFactors = riskFactors!.Where(t => t.GeographicPresenceId.HasValue && t.GeographicPresence!.CountryId == countryId && t.BusinessSegmentId.HasValue && t.BusinessSegment!.Name == customerSegmentName).ToList();
                    //Sub Factor responses
                    riskFactorIds = model.RiskFactors.Select(t => t.Id).ToList();
                    var subFactors = (await _riskAssessmentManager.GetRiskSubFactorsByRiskType(customerId, riskFactorIds))
                         .OrderBy(t => t.Sequence).ThenBy(t => t.Name).Distinct().ToList();
                    if (subFactors != null && subFactors.Any())
                    {
                        model.RiskSubFactors = subFactors;
                        var subFactorResponses = await _riskAssessmentManager.GetRiskSubFactorResponse(customerId, riskFactorIds);
                        foreach (var response in subFactorResponses)
                        {
                            var itemSubFactor = subFactors.Where(t => t.RiskFactorId == response.RiskFactorId && t.Id == response.RiskSubFactorId).FirstOrDefault();
                            if (itemSubFactor != null)
                            {
                                itemSubFactor.Score = response.Score;
                                itemSubFactor.Assumptions = response.Assumptions;
                                if (itemSubFactor.RiskFactor!.RiskRangeParameter == RiskRangeParameter.Scale)
                                {
                                    if (response.Response.HasValue && response.Response.Value > 0)
                                        itemSubFactor.Response = Convert.ToInt32(response.Response);
                                }
                                else
                                    itemSubFactor.Response = response.Response;
                                itemSubFactor.PreDefinedParameterId = response.PreDefinedParameterId;
                                itemSubFactor.ResponseDescription = response.ResponseDescription;
                                itemSubFactor.RiskRangeParameter = itemSubFactor.RiskFactor!.RiskRangeParameter;
                            }
                        }
                    }
                }
               
            }
            ViewBag.Groups = groupList;

            if (groupList.Contains(5) && groupList.Contains(4) && groupList.Contains(3) && isDrillDown == false)
                return PartialView("_SummaryJurisdictionCustomerBusinessSegmentPartial", riskFactors);

            if (groupList.Contains(5) && groupList.Contains(4) && isDrillDown == false)
                return PartialView("_SummaryCustomerBusinessSegmentPartial", riskFactors);

            if (groupList.Contains(5) && isDrillDown == false)
                return PartialView("_SummaryBusinessSegmentPartial", riskFactors);

            if (groupList.Contains(4) && groupList.Contains(3) && isDrillDown == false)
                return PartialView("_SummaryJurisdictionCustomerSegmentPartial", riskFactors);

            if (groupList.Contains(4) && isDrillDown == false)
                return PartialView("_SummaryCustomerSegmentPartial", riskFactors);

            if (groupList.Contains(3) && isDrillDown == false)
                return PartialView("_SummaryJurisdictionPartial", riskFactors);


            if (groupList.Contains(6) && !string.IsNullOrEmpty(customerSegmentName) && isDrillDown == true)
            {
                return PartialView("_SummaryRiskDetailsDrillPartial", model);
            }
            if (groupList.Contains(5) && countryId > 0 && !string.IsNullOrEmpty(customerSegmentName) && isDrillDown == true)
            {
                riskFactors = riskFactors!.Where(t => t.GeographicPresenceId.HasValue && t.GeographicPresence!.CountryId == countryId && t.CustomerSegmentId.HasValue && t.CustomerSegment!.Name == customerSegmentName).ToList();
                return PartialView("_SummaryBusinessSegmentDrillPartial", riskFactors);
            }

            if (groupList.Contains(4) && countryId > 0 && isDrillDown == true)
            {
                riskFactors = riskFactors!.Where(t => t.GeographicPresenceId.HasValue && t.GeographicPresence!.CountryId == countryId).ToList();
                return PartialView("_SummaryJurisdictionCustomerSegmentDrillPartial", riskFactors);
            }

            if (groupList.Contains(3) && isDrillDown == true)
            {
                return PartialView("_SummaryJurisdictionDrillPartial", riskFactors);
            }
            if (groupList.Contains(2))
                return PartialView("_SummaryGroupPartial", riskFactors);

            return PartialView("Index", riskFactors);
        }
        public async Task<IActionResult> DashboardDetails(string riskTpye, string group)
        {
            ViewBag.ScaleType = GetUserCustomerScale();

            var riskTypeList = riskTpye.Decrypt().Split(',').Select(t => Convert.ToInt32(t)).ToList();
            var groupList = group.Split(',').Select(t => Convert.ToInt32(t)).ToList();
            ViewBag.Groups = groupList;
            var customerId = GetUserCustomerId();

            List<RiskFactorViewModel> riskFactors = (await _riskFactorManager.GetAsync(customerId
                , new[] { "Stage", "RiskType", "GeographicPresence", "GeographicPresence.Country", "CustomerSegment", "BusinessSegment" }
                , t => t.IsActive))
                .OrderBy(t => t.Stage!.Sequence).ThenBy(t => t.Stage!.Name)
                .OrderBy(t => t.RiskType!.Sequence).ThenBy(t => t.RiskType!.Name)
                .OrderBy(t => t.GeographicPresence?.Sequence).ThenBy(t => t.GeographicPresence?.CountryName)
                .OrderBy(t => t.CustomerSegment?.Sequence).ThenBy(t => t.CustomerSegment?.Name)
                .OrderBy(t => t.BusinessSegment?.Sequence).ThenBy(t => t.BusinessSegment?.Name)
                .ThenBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            riskFactors = riskFactors.Where(t => riskTypeList.Contains(t.RiskTypeId!.Value)).ToList();
            List<int> riskFactorIds = new();
            if (riskFactors != null && riskFactors.Any())
            {
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
                //Factor responses
                var riskFactorResponses = await _riskAssessmentManager.GetRiskFactorResponse(customerId, riskFactorIds);
                if (riskFactorResponses != null && riskFactorResponses.Any())
                {
                    foreach (var factor in riskFactors)
                    {
                        var response = riskFactorResponses.Where(t => t.RiskFactorId == factor.Id).FirstOrDefault();
                        if (response != null)
                        {
                            factor.TotalWeightedScore = response.TotalWeightedScore;
                        }
                    }
                }
            }
            ViewBag.Groups = groupList;
            await Task.CompletedTask;
            var stages = riskFactors.Select(t => t.Stage).DistinctBy(t => t!.Id).OrderBy(t => t.Sequence).ToList();
            List<SummaryWeightViewModel> RiskStageList = new();
            Dictionary<int, decimal> stageScores = new();



            foreach (var stage in stages)
            {
                var uniqueFactors = riskFactors.Where(t => t.StageId == stage!.Id).Select(t => t.Name).Distinct();
                var totalWeightedScoreIR = 0.00M;
                List<SummaryWeightViewModel> factorList = new();

                foreach (var factorName in uniqueFactors)
                {
                    var factorScores = riskFactors.Where(t => t.StageId == stage!.Id && t.Name == factorName);
                    var weigthPercent = 0.00M;
                    var avgScore = 0.00M;
                    if (factorScores.Any())
                    {
                        weigthPercent = factorScores.First().WeightPercentage;
                        avgScore = factorScores.Average(t => t.TotalWeightedScore);
                    }
                    factorList.Add(new()
                    {
                        Category = factorName,
                        WeightPercentage = weigthPercent,
                        WeightedScore = avgScore
                    });

                }
                foreach (var factor in factorList)
                {
                    var weightedScore = factor.WeightedScore * factor.WeightPercentage / 100;
                    totalWeightedScoreIR += weightedScore;
                }
                stageScores.TryAdd(stage.Id, totalWeightedScoreIR);

            }

            if (stages.Any() && stages.Count > 1 && stageScores.Any() && stageScores.Count > 1)
            {
                foreach (var stage in stages)
                {
                    var stageScore = stageScores.Where(t => t.Key == stage!.Id).FirstOrDefault();
                    RiskStageList.Add(new()
                    {
                        Category = @stageScore.Value.ToTwoDecimal()
                    });
                    ViewBag.StageVal = RiskStageList;
                }
            }

            return PartialView("_DashboardPartial", riskFactors);
        }
        public async Task<IActionResult> Comparison(int fromVersionId = 0, int toVersionId = 0)
        {
            var approvedVersionlist = await _riskAssessmentManager.GetApprovedVersion(GetUserCustomerId());
            ViewBag.ApprovedVersion = approvedVersionlist;

            return PartialView("_RiskComparePartial");
        }

        public async Task<IActionResult> GetRiskFactorComparison(int fromVersionId, int toVersionId, string riskTpye, string group)
        {

            var riskTypeList = riskTpye.Decrypt().Split(',').Select(t => Convert.ToInt32(t)).ToList();
            var groupList = group.Split(',').Select(t => Convert.ToInt32(t)).ToList();
            ViewBag.Groups = groupList;
            var customerId = GetUserCustomerId();

            List<RiskFactorViewModel> riskFactors = (await _riskFactorManager.GetAsync(customerId
                , new[] { "Stage", "RiskType", "GeographicPresence", "GeographicPresence.Country", "CustomerSegment", "BusinessSegment" }
                , t => t.IsActive))
                .OrderBy(t => t.Stage!.Sequence).ThenBy(t => t.Stage!.Name)
                .OrderBy(t => t.RiskType!.Sequence).ThenBy(t => t.RiskType!.Name)
                .OrderBy(t => t.GeographicPresence?.Sequence).ThenBy(t => t.GeographicPresence?.CountryName)
                .OrderBy(t => t.CustomerSegment?.Sequence).ThenBy(t => t.CustomerSegment?.Name)
                .OrderBy(t => t.BusinessSegment?.Sequence).ThenBy(t => t.BusinessSegment?.Name)
                .ThenBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            riskFactors = riskFactors.Where(t => riskTypeList.Contains(t.RiskTypeId!.Value)).ToList();
            List<int> riskFactorIds = new();
            if (riskFactors != null && riskFactors.Any())
            {
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
                //Factor responses
                var riskFactorResponses = await _riskAssessmentManager.GetRiskFactorResponse(customerId, riskFactorIds);
                if (riskFactorResponses != null && riskFactorResponses.Any())
                {
                    foreach (var factor in riskFactors)
                    {
                        var response = riskFactorResponses.Where(t => t.RiskFactorId == factor.Id).FirstOrDefault();
                        if (response != null)
                        {
                            factor.TotalWeightedScore = response.TotalWeightedScore;
                        }
                    }
                }
                //From version Factor responses
                var fromriskFactorResponses = await _riskAssessmentManager.GetApprovedRiskFactorResponse(customerId, riskFactorIds, fromVersionId);
                if (fromriskFactorResponses != null && fromriskFactorResponses.Any())
                {
                    foreach (var factor in riskFactors)
                    {
                        var response = fromriskFactorResponses.Where(t => t.RiskFactorId == factor.Id).FirstOrDefault();
                        if (response != null)
                        {
                            factor.FromVersionTotalWeightedScore = response.TotalWeightedScore;
                        }
                    }
                }
                //To version Factor responses
                var toriskFactorResponses = await _riskAssessmentManager.GetApprovedRiskFactorResponse(customerId, riskFactorIds, toVersionId);
                if (toriskFactorResponses != null && toriskFactorResponses.Any())
                {
                    foreach (var factor in riskFactors)
                    {
                        var response = toriskFactorResponses.Where(t => t.RiskFactorId == factor.Id).FirstOrDefault();
                        if (response != null)
                        {
                            factor.ToVersionTotalWeightedScore = response.TotalWeightedScore;
                        }
                    }
                }
            }

            if (groupList.Contains(5) && groupList.Contains(4) && groupList.Contains(3))
                return PartialView("_CompareJurisdictionCustomerBusinessSegmentPartial", riskFactors);

            if (groupList.Contains(5) && groupList.Contains(4))
                return PartialView("_CompareCustomerBusinessSegmentPartial", riskFactors);

            if (groupList.Contains(5))
                return PartialView("_CompareBusinessSegmentPartial", riskFactors);

            if (groupList.Contains(4) && groupList.Contains(3))
                return PartialView("_CompareJurisdictionCustomerSegmentPartial", riskFactors);

            if (groupList.Contains(4))
                return PartialView("_CompareCustomerSegmentPartial", riskFactors);

            if (groupList.Contains(3))
                return PartialView("_CompareJurisdictionPartial", riskFactors);
            if (groupList.Contains(2))
                return PartialView("_RiskComparisonPartial", riskFactors);

            return PartialView("Index", riskFactors);
        }

        public ActionResult Sector()
        {
            return View();
        }
    }
}
