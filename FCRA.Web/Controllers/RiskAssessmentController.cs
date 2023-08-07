using FCRA.Common;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.Repository.Managers;
using FCRA.Utilities;
using FCRA.ViewModels;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Responses;
using FCRA.Web.Models;
using FCRA.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Security.Cryptography;

namespace FCRA.Web.Controllers
{
    [ValidateFormAccess(Common.FormDefination.RiskAssessment)]
    [CheckClaim(Common.Constants.UserCusermerId)]
    public class RiskAssessmentController : BaseController
    {
        private readonly IMasterManagerCustomer<StageViewModel> _stageManager;
        private readonly IMasterManagerCustomer<RiskTypeViewModel> _riskTypeManager;
        private readonly IMasterManagerCustomer<GeographicPresenceViewModel> _geographicPresenceManager;
        private readonly IMasterManagerCustomer<CustomerSegmentViewModel> _customerSegmentManager;
        private readonly IMasterManagerCustomer<BusinessSegmentViewModel> _businessSegmentManager;
        private readonly IMasterManagerCustomer<RiskFactorViewModel> _riskFactorManager;
        private readonly IRiskAssessmentManager _riskAssessmentManager;
        private readonly IProductServiceMappingManager _productServiceMappingManager;
        private readonly IProductRiskCriteriaMappingManager _riskCriteriaMappingManager;
        private readonly IMasterManager<CountryViewModel> _countryManager;
        private readonly IMasterManagerCustomer<GeographyRiskViewModel> _geographyRiskManager;
        private readonly IReportManager _reportManager;
        private readonly StorageSettings _storageSettings;
        private readonly EmailSettings _EmailSettings;
        private readonly IApprovalMatrixManager _approverMatrixManager;

        public RiskAssessmentController(IMasterManagerCustomer<StageViewModel> stageManager
            , IMasterManagerCustomer<RiskTypeViewModel> riskTypeManager
            , IMasterManagerCustomer<GeographicPresenceViewModel> geographicPresenceManager
            , IMasterManagerCustomer<CustomerSegmentViewModel> customerSegmentManager
            , IMasterManagerCustomer<BusinessSegmentViewModel> businessSegmentManager

            , IMasterManagerCustomer<RiskFactorViewModel> riskFactorManager, IRiskAssessmentManager riskAssessmentManager
            , IProductServiceMappingManager productServiceMappingManager, IProductRiskCriteriaMappingManager riskCriteriaMappingManager
            , IMasterManager<CountryViewModel> countryManager
             , IMasterManagerCustomer<GeographyRiskViewModel> geographyRiskManager
            , IReportManager reportManager, IOptions<StorageSettings> storageSettings, IOptions<EmailSettings> emailSettings, IApprovalMatrixManager approverMatrixManager
           )
        {
            _stageManager = stageManager;
            _riskTypeManager = riskTypeManager;
            _geographicPresenceManager = geographicPresenceManager;
            _customerSegmentManager = customerSegmentManager;
            _businessSegmentManager = businessSegmentManager;
            _riskFactorManager = riskFactorManager;
            _riskAssessmentManager = riskAssessmentManager;
            _productServiceMappingManager = productServiceMappingManager;
            _riskCriteriaMappingManager = riskCriteriaMappingManager;
            _countryManager = countryManager;
            _geographyRiskManager = geographyRiskManager;
            _reportManager = reportManager;
            _storageSettings = storageSettings.Value;
            _EmailSettings = emailSettings.Value;
            _approverMatrixManager = approverMatrixManager;
        }
        public async Task<IActionResult> Index()
        {
            //string[]? includes = null;
            //bool isTreeViewType = IsTreeViewType();
            //if (isTreeViewType)
            //    includes = new[] { "RiskFactors" };
            //var riskTypes = await _riskTypeManager.GetAsync(GetUserCustomerId(), includes, t => t.IsActive);
            //ViewBag.IsTreeViewType = isTreeViewType;
            //return View(riskTypes);
            var userType = GetUserType();
            var stages = (await _stageManager.GetAsync(GetUserCustomerId(), null, t => t.IsActive)).OrderBy(t => t.Sequence).ThenBy(t => t.Name);
            if (userType == 4 || userType == 5)
            {
                var approvalMatrix = await _approverMatrixManager.GetApprovalMatrixAccess(GetUserCustomerId(), GetUserId());
                var StageAccessed = stages.Select(s1 => s1.Id).ToList().Intersect(approvalMatrix.Select(s2 => s2.StageId).ToList()).ToList();
                if (StageAccessed != null && stages != null)
                    stages = stages.Where(t => StageAccessed.Contains(t.Id)).OrderBy(t => t.Sequence);

            }
            ViewBag.UserTypes = userType;
            ViewBag.ScaleType = GetUserCustomerScale();
            ViewBag.RegisterCompletion = await _reportManager.RiskRegisterCompletionGet(GetUserCustomerId(), 0, 1);
            ViewBag.customerId = GetUserCustomerId();
            return View("IndexPills", stages);
        }

        public async Task<IActionResult> Assessment(string t)
        {
            if (!Int32.TryParse(t.Decrypt(), out int typeIdInt) || typeIdInt <= 0)
                return NotFound();
            var model = await GetRiskResponseModels(typeIdInt, new List<int>());
            if (model == null || model.RiskType == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveResponse(ResponseSaveViewModel items, int stageId, int? riskTypeId
            , int? geoPresenceId, int? customerSegmentId, int? businessSegmentId, string? riskFactor)
        {
            if (items == null || (!items.RiskScoreResponses.Any() && !items.RiskSubFactorResponses.Any()
                && !items.RiskFactorResponses.Any() && !items.RiskScoreProductVolumRatingResponses.Any()
                && !items.VolumeMappings.Any()))
            {
                return Json(new AppResultViewModel { Status = false, Message = "" });
            }
            if (stageId <= 0)
                return Json(new AppResultViewModel { Status = false, Message = "" });
            List<int> riskFactorIds = new();
            var customerId = GetUserCustomerId();
            var riskFactors = await _riskFactorManager.GetAsync(customerId, null,
                t => t.StageId == stageId && ((!riskTypeId.HasValue && !t.RiskTypeId.HasValue) || (t.RiskTypeId == riskTypeId))
                && ((!geoPresenceId.HasValue && !t.GeographicPresenceId.HasValue) || (t.GeographicPresenceId == geoPresenceId))
                && ((!customerSegmentId.HasValue && !t.CustomerSegmentId.HasValue) || (t.CustomerSegmentId == customerSegmentId))
                && ((!businessSegmentId.HasValue && !t.BusinessSegmentId.HasValue) || (t.BusinessSegmentId == businessSegmentId)));

            //For single risk factor update
            if (int.TryParse(riskFactor?.Decrypt(), out int riskFactorId) && riskFactorId > 0)
                riskFactors = riskFactors.Where(t => t.Id == riskFactorId).ToList();

            if (riskFactors != null && riskFactors.Any())
            {
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
            }
            var result = await _riskAssessmentManager.UpdateAssessmentResponse(customerId, items, riskFactorIds, this.GetUserId());
            return Json(new AppResultViewModel { Status = result, Message = result ? "Responses updated successfully" : "Something went wrong!" });
        }

        [HttpPost]
        public async Task<IActionResult> GetQuestionRiskCriteriaMapping(int riskFactorId, int riskSubFactorId, int productId, int riskCriteriaId, string? questionId, string? answers)
        {
            var list = await _riskAssessmentManager.GetQuestionRiskCriteriaMapping(GetUserCustomerId(), riskFactorId, riskSubFactorId, productId, riskCriteriaId);
            if (!string.IsNullOrEmpty(questionId) && !string.IsNullOrWhiteSpace(answers))
            {
                List<int> qIds = questionId.Split(',').Select(t => Convert.ToInt32(t)).ToList();
                List<int> aIds = answers.Split(',').Select(t => Convert.ToInt32(t)).ToList();
                if (qIds.Count == aIds.Count)
                {
                    for (int i = 0; i < qIds.Count; i++)
                    {
                        var item = list.Where(t => t.QuestionId == qIds[i]).FirstOrDefault();
                        if (item != null)
                        {
                            item.SelectedRating = aIds[i];
                        }
                    }
                }
            }
            ViewBag.ScaleType = GetUserCustomerScale();
            return PartialView("_QuestionListPartial", list);
        }

        public async Task<IActionResult> GetCountries(string? countries, string? ratings, string? volumes)
        {
            var list = (await _geographyRiskManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.IsActive)).OrderBy(t => t.Country!.Name);
            List<CountryVolumeResponseViewModel> countryList = new();
            foreach (var item in list)
            {
                countryList.Add(new()
                {
                    Id = item.Id,
                    Name = item.Country?.Name,
                    RiskRating = item.RiskRating
                });
            }
            if (!string.IsNullOrWhiteSpace(countries) && !string.IsNullOrWhiteSpace(ratings) && !string.IsNullOrWhiteSpace(volumes))
            {
                List<int> cList = countries.Split(',').Select(t => Convert.ToInt32(t)).ToList();
                List<int> ratingList = ratings.Split(',').Select(t => Convert.ToInt32(t)).ToList();
                List<decimal> volumeList = volumes.Split(',').Select(t => Convert.ToDecimal(t)).ToList();
                if (cList.Count == ratingList.Count && ratingList.Count == volumeList.Count)
                {
                    for (int i = 0; i < cList.Count; i++)
                    {
                        var country = countryList.Where(t => t.Id == cList[i]).FirstOrDefault();
                        if (country != null)
                        {
                            country.RiskRating = (RiskRating)ratingList[i];
                            country.Volume = volumeList[i];
                        }
                    }
                }
            }

            return PartialView("_CountryListPartial", countryList);
        }

        public async Task<IActionResult> RAssessment(string? t, string? f)
        {
            if (!int.TryParse(t?.Decrypt(), out int tId) || tId <= 0
                || !int.TryParse(f?.Decrypt(), out int fId) || fId <= 0)
                return NotFound();
            var model = await GetRiskResponseModels(tId, new List<int> { fId });

            return View(model);
        }

        public async Task<IActionResult> D(string? s, string? t, string? g, string? c, string? b)

        {
            if (!int.TryParse(t?.Decrypt(), out int tId) || tId <= 0
              || !int.TryParse(s?.Decrypt(), out int sId) || sId <= 0)
                return NotFound();
            int? gId, cId, bId;
            gId = cId = bId = null;
            if (int.TryParse(g?.Decrypt(), out int gId1))
                gId = gId1;
            if (int.TryParse(c?.Decrypt(), out int cId1))
                cId = cId1;
            if (int.TryParse(b?.Decrypt(), out int bId1))
                bId = bId1;

            var scaleType = (ScaleType)GetUserCustomerScale();
            var model = await GetRiskResponseModelsPills(sId, tId, gId, cId, bId);
            DataSet ds = new();
            IDictionary<string, List<int>> hiddenColumns = new Dictionary<string, List<int>>();
            var fileName = GetExcelFileName(model);
            var dt = GetRiskExcelTemplateFactor(model, fileName, out List<int> lockedRows);
            ds.Tables.Add(dt);
            hiddenColumns.Add(dt.TableName, new() { 2, 3, 4, 5, 6, 7, 8, 9 });

            if (model.ProductServiceMappings.Any())
            {
                var dt2 = GetRiskExcelTemplateProductSubFactor(model);
                if (dt2 != null)
                {
                    ds.Tables.Add(dt2);
                    hiddenColumns.Add(dt2.TableName, new() { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
                }
            }

            if (model.RiskFactors.Any(t => t.RiskRangeParameter == RiskRangeParameter.Volume))
            {
                var dt1 = GetRiskExcelTemplateVolumeSubFactor(model, scaleType);
                foreach (var tbl in dt1)
                {
                    ds.Tables.Add(tbl);
                    hiddenColumns.Add(tbl.TableName, new() { 2, 3, 4, 5, 6, 7, 8, 9, 10 });
                }

            }

            var stream = ExcelXlsxHelper.ToExcel(ds, $"{model.RiskType.Name}-{model.RiskFactors.FirstOrDefault()?.Name}", 0, false, false, true, hiddenColumns);
            return File(stream, "application/vnd.ms-excel", $"{fileName} {DateTime.UtcNow.UTCToIST():yyyy-MM-dd_HHmm}.xlsx");
        }
        private async Task<AssessmentViewModel> GetRiskResponseModels(int typeId, List<int> riskFactorIds)
        {
            var riskType = await _riskTypeManager.GetAsync(typeId);
            if (riskType == null)
                return new AssessmentViewModel();

            AssessmentViewModel model = new();
            //model.RiskType = riskType;

            List<RiskFactorViewModel> riskFactors = await _riskFactorManager.GetAsync(GetUserCustomerId(), null, t => t.RiskTypeId == typeId);
            if (riskFactorIds.Any())
                riskFactors = riskFactors.Where(t => riskFactorIds.Contains(t.Id)).ToList();
            if (riskFactors != null && riskFactors.Any())
            {
                model.RiskFactors = riskFactors;
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
                //Factor responses
                var riskFactorResponses = await _riskAssessmentManager.GetRiskFactorResponse(GetUserCustomerId(), riskFactorIds);
                if (riskFactorResponses != null && riskFactorResponses.Any())
                {
                    foreach (var factor in model.RiskFactors)
                    {
                        var response = riskFactorResponses.Where(t => t.RiskFactorId == factor.Id).FirstOrDefault();
                        if (response != null)
                        {
                            factor.TotalWeightedScore = response.TotalWeightedScore;
                        }
                    }
                }

                //Sub Factor responses
                var subFactors = await _riskAssessmentManager.GetRiskSubFactorsByRiskType(GetUserCustomerId(), riskFactorIds);
                if (subFactors != null && subFactors.Any())
                {
                    model.RiskSubFactors = subFactors;
                    var subFactorResponses = await _riskAssessmentManager.GetRiskSubFactorResponse(GetUserCustomerId(), riskFactorIds);
                    foreach (var response in subFactorResponses)
                    {
                        var itemSubFactor = model.RiskSubFactors.Where(t => t.RiskFactorId == response.RiskFactorId && t.Id == response.RiskSubFactorId).FirstOrDefault();
                        if (itemSubFactor != null)
                        {
                            itemSubFactor.Score = response.Score;
                            itemSubFactor.Assumptions = response.Assumptions;
                            itemSubFactor.Response = response.Response;
                            itemSubFactor.PreDefinedParameterId = response.PreDefinedParameterId;
                            itemSubFactor.ResponseDescription = response.ResponseDescription;
                            itemSubFactor.RiskRangeParameter = response.RiskRangeParameter;
                        }
                    }
                }
                //Sub Factor Volume mapping
                model.VolumeMappings = await _riskAssessmentManager.GetRiskSubFactorVolumeResponse(GetUserCustomerId(), riskFactorIds);
                foreach (var subFactor in model.RiskSubFactors)
                {
                    if (subFactor.RiskRangeParameter == Common.RiskRangeParameter.Volume && !model.VolumeMappings.Any(t => t.RiskFactorId == subFactor.RiskFactorId && t.RiskSubFactorId == subFactor.Id))
                    {
                        model.VolumeMappings.Add(new()
                        {
                            RiskFactorId = subFactor.RiskFactorId,
                            RiskSubFactorId = subFactor.Id,
                            Score1 = subFactor.RiskVolume1,
                            Score2 = subFactor.RiskVolume2,
                            Score3 = subFactor.RiskVolume3
                        });
                    }
                }
            }
            var productServiceMapping = await _productServiceMappingManager.GetAsync(GetUserCustomerId(), new[] { "ProductService" }, t => t.IsActive);
            if (productServiceMapping != null && productServiceMapping.Any())
            {
                model.ProductServiceMappings = productServiceMapping;
                var productVolumResponse = await _riskAssessmentManager.GetRiskScoreProductVolumRatingResponse(GetUserCustomerId(), riskFactorIds);
                if (productVolumResponse != null && productVolumResponse.Any())
                {
                    foreach (var service in model.ProductServiceMappings)
                    {
                        var response = productVolumResponse.Where(t => t.RiskFactorId == service.RiskFactorId && t.RiskSubFactorId == service.RiskSubFactorId && t.ProductId == service.ProductId).FirstOrDefault();
                        if (response != null)
                        {
                            service.Volume = response.Volume;
                            service.TotalScore = response.TotalScore;
                            service.FinalScore = response.FinalScore;
                            service.RiskRating = response.RiskRating;
                        }
                    }
                }
            }
            var riskCriteria = await _riskCriteriaMappingManager.GetAsync(GetUserCustomerId(), new[] { "ProductService", "RiskCriteria" }, t => t.IsActive);
            if (riskCriteria != null && riskCriteria.Any())
            {
                var scores = await _riskAssessmentManager.GetRiskScoreResponse(GetUserCustomerId(), riskFactorIds);
                foreach (var criteria in riskCriteria)
                {
                    var score = scores.Where(t => t.RiskFactorId == criteria.RiskFactorId && t.ProductId == criteria.ProductId && t.RiskCriteriaId == criteria.RiskCriteriaId).FirstOrDefault();
                    RiskScoreResponseViewModel tempScore = new()
                    {
                        RiskFactorId = criteria.RiskFactorId,
                        RiskSubFactorId = criteria.RiskSubFactorId,
                        ProductId = criteria.ProductId,
                        RiskCriteriaId = criteria.RiskCriteriaId,
                        RiskFactor = criteria.RiskFactor,
                        ProductService = criteria.ProductService,
                        RiskCriteria = criteria.RiskCriteria
                    };
                    if (score != null)
                    {
                        tempScore.Score = score.Score;
                        tempScore.QuestionIds = score.QuestionIds;
                        tempScore.Answers = score.Answers;
                    }
                    model.RiskCriteriaMappings.Add(tempScore);
                }
            }
            return model;
        }

        private DataSet GetRiskExcelTemplate(AssessmentPillsViewModel model)
        {
            DataSet ds = new();
            //Subfactor
            DataTable dtSubFactor = new()
            {
                TableName = "SubFactorResponse"
            };
            dtSubFactor.Columns.Add(new DataColumn("SL.No.", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("RiskType", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Factor", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("SubFactor", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Description", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Low", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Medium", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("High", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("RiskWeightage", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Response", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Comments", typeof(string)));
            var iIndex = 1;

            foreach (var subFactor in model.RiskSubFactors)
            {

                var factor = model.RiskFactors.FirstOrDefault(x => x.Id == subFactor.RiskFactorId);
                DataRow dr = dtSubFactor.NewRow();
                dr["SL.No."] = iIndex;
                dr["RiskType"] = model.RiskType?.Name;
                dr["Factor"] = factor?.Name;
                dr["SubFactor"] = subFactor.Name;
                dr["Description"] = subFactor.Description;
                if (subFactor.IsExcludedInRisk)
                {
                    dr["Low"] = "NA";
                    dr["Medium"] = "NA";
                    dr["High"] = "NA";
                    dr["RiskWeightage"] = "NA";
                    dr["Response"] = "NA";
                }
                else
                {
                    if (subFactor.RiskRangeParameter == Common.RiskRangeParameter.PercentRange)
                    {
                        dr["Low"] = $"{subFactor.Percentage2}%";
                        dr["Medium"] = $"{subFactor.Percentage3} - {subFactor.Percentage4}%";
                        dr["High"] = $"{subFactor.Percentage5}%";
                        dr["Response"] = subFactor.Response;
                    }
                    else if (subFactor.RiskRangeParameter == Common.RiskRangeParameter.PreDefinedParameters)
                    {
                        dr["Low"] = subFactor.PreDefinedRiskParameter1?.Name;
                        dr["Medium"] = subFactor.PreDefinedRiskParameter2?.Name;
                        dr["High"] = subFactor.PreDefinedRiskParameter3?.Name;
                        if (subFactor.PreDefinedParameterId == subFactor.PreDefinedParameter1Id)
                            dr["Response"] = subFactor.PreDefinedRiskParameter1?.Name;
                        else if (subFactor.PreDefinedParameterId == subFactor.PreDefinedParameter2Id)
                            dr["Response"] = subFactor.PreDefinedRiskParameter2?.Name;
                        else if (subFactor.PreDefinedParameterId == subFactor.PreDefinedParameter3Id)
                            dr["Response"] = subFactor.PreDefinedRiskParameter3?.Name;
                    }
                    else if (subFactor.RiskRangeParameter == Common.RiskRangeParameter.Descriptive)
                    {
                        dr["Low"] = subFactor.RiskDescription1;
                        dr["Medium"] = subFactor.RiskDescription2;
                        dr["High"] = subFactor.RiskDescription3;
                        if (subFactor.ResponseDescription == subFactor.RiskDescription1)
                            dr["Response"] = subFactor.RiskDescription1;
                        else if (subFactor.ResponseDescription == subFactor.RiskDescription2)
                            dr["Response"] = subFactor.RiskDescription2;
                        else if (subFactor.ResponseDescription == subFactor.RiskDescription3)
                            dr["Response"] = subFactor.RiskDescription3;
                    }
                    else if (subFactor.RiskRangeParameter == Common.RiskRangeParameter.Volume)
                    {
                        dr["Low"] = "-";
                        dr["Medium"] = "-";
                        dr["High"] = "-";
                        dr["Response"] = subFactor.Response;
                    }
                    dr["RiskWeightage"] = $"{subFactor.RiskWeightage}%";
                }
                dr["Comments"] = subFactor.Assumptions;
                dtSubFactor.Rows.Add(dr);
                iIndex += 1;

                //risk criteria
                var subFactorCriteriasData = FCRA.ViewModels.AssessmentPillsViewModel.GetAssesmentModel(model, factor.Id);
                if (subFactorCriteriasData.RiskCriteriaMappings.Any())
                {
                    DataTable dtCriteria = new()
                    {
                        TableName = "Criterias"
                    };
                    dtCriteria.Columns.Add(new DataColumn("SL.No.", typeof(string)));
                    dtCriteria.Columns.Add(new DataColumn("RiskType", typeof(string)));
                    dtCriteria.Columns.Add(new DataColumn("Factor", typeof(string)));
                    dtCriteria.Columns.Add(new DataColumn("SubFactor", typeof(string)));
                    dtCriteria.Columns.Add(new DataColumn("Product", typeof(string)));
                    var colVolume = new DataColumn("Volume", typeof(decimal))
                    {
                        AllowDBNull = true
                    };
                    dtCriteria.Columns.Add(colVolume);
                    var criterias = subFactorCriteriasData.RiskCriteriaMappings.Select(t => new FCRA.ViewModels.SelectIntViewModel { Id = t.RiskCriteria.Id, Name = t.RiskCriteria.Name }).DistinctBy(t => t.Id).ToList();
                    foreach (var criteria in criterias)
                    {
                        var colCr = new DataColumn(criteria.Name, typeof(decimal))
                        {
                            AllowDBNull = true
                        };
                        dtCriteria.Columns.Add(colCr);
                    }
                    iIndex = 0;
                    foreach (var subFactor1 in model.RiskSubFactors)
                    {
                        var subFactorCriterias = FCRA.ViewModels.AssessmentPillsViewModel.GetAssesmentModel(model, factor.Id, subFactor1.Id);
                        foreach (var product in subFactorCriterias.ProductServiceMappings)
                        {
                            var hasRisks = subFactorCriterias.RiskCriteriaMappings.Any(t => t.RiskFactorId == product.RiskFactorId && t.ProductId == product.ProductId);
                            if (hasRisks)
                            {
                                DataRow drCriteria = dtCriteria.NewRow();
                                iIndex += 1;
                                drCriteria["SL.No."] = iIndex;
                                drCriteria["RiskType"] = model?.RiskType?.Name;
                                drCriteria["Factor"] = factor?.Name;
                                drCriteria["SubFactor"] = subFactor1.Name;
                                drCriteria["Product"] = product.ProductService?.Name;
                                if (product.Volume.HasValue)
                                    drCriteria["Volume"] = product.Volume;
                                foreach (var criteria in criterias)
                                {
                                    var risk = subFactorCriterias.RiskCriteriaMappings.Where(t => t.RiskFactorId == product.RiskFactorId && t.ProductId == product.ProductId && t.RiskCriteriaId == criteria.Id).FirstOrDefault();
                                    if (risk != null && risk.Score.HasValue)
                                    {
                                        drCriteria[criteria.Name] = risk.Score;
                                    }
                                }
                                dtCriteria.Rows.Add(drCriteria);
                            }
                        }
                        iIndex = 0;
                    }
                    ds.Tables.Add(dtCriteria);
                }

            }
            ds.Tables.Add(dtSubFactor);
            //if (factor == null)
            //    return ds;

            //risk volume

            return ds;
        }

        [HttpPost]
        public async Task<IActionResult> AssessmentPills(string stage, string? riskType, string? geoPresence, string? customerSegment, string? businessSegment)
        {
            int? riskTypeId = null;
            int? geoPresenceId = null;
            int? customerSegmentId = null;
            int? businessSegmentId = null;
            int customerId = GetUserCustomerId();

            if (!Int32.TryParse(stage.Decrypt(), out int stageId) || stageId <= 0)
                return NotFound();
            if (Int32.TryParse(riskType.Decrypt(), out int riskTypeIdInt))
                riskTypeId = riskTypeIdInt;
            if (Int32.TryParse(geoPresence.Decrypt(), out int geoPresenceIdInt))
                geoPresenceId = geoPresenceIdInt;
            if (Int32.TryParse(customerSegment.Decrypt(), out int customerSegmentIdInt))
                customerSegmentId = customerSegmentIdInt;
            if (Int32.TryParse(businessSegment.Decrypt(), out int businessSegmentIdInt))
                businessSegmentId = businessSegmentIdInt;


            var model = await GetRiskResponseModelsPills(stageId, riskTypeId, geoPresenceId, customerSegmentId, businessSegmentId);
            if (model == null || model.Stage == null)
                return NotFound();
            ViewBag.UserType = GetUserType();
            ViewBag.ScaleType = GetUserCustomerScale();
            ViewBag.IsThroughSMTP = _EmailSettings.IsThroughSMTP;
            ViewBag.RegisterCompletion = await _reportManager.RiskRegisterCompletionGet(customerId, 0, 1);
            ViewBag.CurrentUser = GetUserId();
            return PartialView("_AssessmentPillsPartial", model);
        }

        private async Task<AssessmentPillsViewModel> GetRiskResponseModelsPills(int stageId, int? riskTypeId
            , int? geoPresenceId, int? customerSegmentId, int? businessSegmentId)
        {
            int customerId = GetUserCustomerId();
            AssessmentPillsViewModel model = new();

            if (stageId > 0)
                model.Stage = await _stageManager.GetAsync(customerId, stageId);
            if (riskTypeId.HasValue && riskTypeId.Value > 0)
                model.RiskType = await _riskTypeManager.GetAsync(customerId, riskTypeId.Value);
            if (geoPresenceId.HasValue && geoPresenceId.Value > 0)
                model.GeographicPresence = await _geographicPresenceManager.GetAsync(customerId, geoPresenceId.Value, new[] { nameof(GeographicPresenceViewModel.Country) });
            if (customerSegmentId.HasValue && customerSegmentId.Value > 0)
                model.CustomerSegment = await _customerSegmentManager.GetAsync(customerId, customerSegmentId.Value);
            if (businessSegmentId.HasValue && businessSegmentId.Value > 0)
                model.BusinessSegment = await _businessSegmentManager.GetAsync(customerId, businessSegmentId.Value);

            List<int> riskFactorIds = new();
            List<RiskFactorViewModel> riskFactors = (await _riskFactorManager.GetAsync(customerId, null
                , t => t.StageId == stageId && ((!riskTypeId.HasValue && !t.RiskTypeId.HasValue) || (t.RiskTypeId == riskTypeId))
                && ((!geoPresenceId.HasValue && !t.GeographicPresenceId.HasValue) || (t.GeographicPresenceId == geoPresenceId))
                && ((!customerSegmentId.HasValue && !t.CustomerSegmentId.HasValue) || (t.CustomerSegmentId == customerSegmentId))
                && ((!businessSegmentId.HasValue && !t.BusinessSegmentId.HasValue) || (t.BusinessSegmentId == businessSegmentId))))
                .OrderBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
            if (riskFactorIds.Any())
                riskFactors = riskFactors.Where(t => riskFactorIds.Contains(t.Id)).ToList();
            if (riskFactors != null && riskFactors.Any())
            {
                model.RiskFactors = riskFactors;
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
                //Factor responses
                var riskFactorResponses = await _riskAssessmentManager.GetRiskFactorResponse(customerId, riskFactorIds);
                if (riskFactorResponses != null && riskFactorResponses.Any())
                {
                    foreach (var factor in model.RiskFactors)
                    {
                        var response = riskFactorResponses.Where(t => t.RiskFactorId == factor.Id).FirstOrDefault();
                        if (response != null)
                        {
                            factor.TotalWeightedScore = response.TotalWeightedScore;
                        }
                    }
                }

                //Sub Factor responses
                var subFactors = (await _riskAssessmentManager.GetRiskSubFactorsByRiskType(customerId, riskFactorIds))
                     .OrderBy(t => t.Sequence).ThenBy(t => t.Name).ToList();
                if (subFactors != null && subFactors.Any())
                {
                    model.RiskSubFactors = subFactors;
                    var subFactorResponses = await _riskAssessmentManager.GetRiskSubFactorResponse(customerId, riskFactorIds);
                    foreach (var response in subFactorResponses)
                    {
                        var itemSubFactor = model.RiskSubFactors.Where(t => t.RiskFactorId == response.RiskFactorId && t.Id == response.RiskSubFactorId).FirstOrDefault();
                        if (itemSubFactor != null)
                        {
                            itemSubFactor.Score = response.Score;
                            itemSubFactor.Assumptions = response.Assumptions;
                            if (itemSubFactor.RiskFactor.RiskRangeParameter == RiskRangeParameter.Scale)
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
                //Sub Factor Volume mapping
                model.VolumeMappings = await _riskAssessmentManager.GetRiskSubFactorVolumeResponse(customerId, riskFactorIds);
                foreach (var subFactor in model.RiskSubFactors)
                {
                    if (subFactor.RiskFactor!.RiskRangeParameter == Common.RiskRangeParameter.Volume && !model.VolumeMappings.Any(t => t.RiskFactorId == subFactor.RiskFactorId && t.RiskSubFactorId == subFactor.Id))
                    {
                        model.VolumeMappings.Add(new()
                        {
                            RiskFactorId = subFactor.RiskFactorId,
                            RiskSubFactorId = subFactor.Id,
                            Score1 = subFactor.RiskVolume1,
                            Score2 = subFactor.RiskVolume2,
                            Score3 = subFactor.RiskVolume3,
                            Score4 = subFactor.RiskVolume4,
                            Score5 = subFactor.RiskVolume5
                        });
                    }
                }
            }
            var productServiceMapping = await _productServiceMappingManager.GetAsync(customerId, new[] { "ProductService" }, t => t.IsActive);
            if (productServiceMapping != null && productServiceMapping.Any())
            {
                model.ProductServiceMappings = productServiceMapping;
                var productVolumResponse = await _riskAssessmentManager.GetRiskScoreProductVolumRatingResponse(customerId, riskFactorIds);
                if (productVolumResponse != null && productVolumResponse.Any())
                {
                    foreach (var service in model.ProductServiceMappings)
                    {
                        var response = productVolumResponse.Where(t => t.RiskFactorId == service.RiskFactorId && t.RiskSubFactorId == service.RiskSubFactorId && t.ProductId == service.ProductId).FirstOrDefault();
                        if (response != null)
                        {
                            service.Volume = response.Volume;
                            service.TotalScore = response.TotalScore;
                            service.FinalScore = response.FinalScore;
                            service.RiskRating = response.RiskRating;
                            service.Value = response.Value;
                        }
                    }
                }
            }
            var riskCriteria = await _riskCriteriaMappingManager.GetAsync(customerId, new[] { "ProductService", "RiskCriteria" }, t => t.IsActive);
            if (riskCriteria != null && riskCriteria.Any())
            {
                var scores = await _riskAssessmentManager.GetRiskScoreResponse(customerId, riskFactorIds);
                foreach (var criteria in riskCriteria)
                {
                    var score = scores.Where(t => t.RiskFactorId == criteria.RiskFactorId && t.ProductId == criteria.ProductId && t.RiskCriteriaId == criteria.RiskCriteriaId).FirstOrDefault();
                    RiskScoreResponseViewModel tempScore = new()
                    {
                        RiskFactorId = criteria.RiskFactorId,
                        RiskSubFactorId = criteria.RiskSubFactorId,
                        ProductId = criteria.ProductId,
                        RiskCriteriaId = criteria.RiskCriteriaId,
                        RiskFactor = criteria.RiskFactor,
                        ProductService = criteria.ProductService,
                        RiskCriteria = criteria.RiskCriteria
                    };
                    if (score != null)
                    {
                        tempScore.Score = score.Score;
                        tempScore.QuestionIds = score.QuestionIds;
                        tempScore.Answers = score.Answers;
                    }
                    model.RiskCriteriaMappings.Add(tempScore);
                }
            }
            //Approval status
            var approvalStatus = (await _riskAssessmentManager.GetApprovalStatusData(customerId, stageId, riskTypeId, geoPresenceId, customerSegmentId, businessSegmentId)).OrderByDescending(t => t.Id).ToList();
            if (approvalStatus != null && approvalStatus.Any())
            {
                model.ApprovalRequests = approvalStatus;
            }
            var approverList = (await _riskAssessmentManager.GetApproverList(customerId));
            if (approverList != null && approverList.Any())
            {
                model.UserViewModels = approverList;
            }

            return model;
        }
        //private DataTable GetRiskExcelTemplateFactor(AssessmentPillsViewModel model, string Name)
        //{
        //    //Subfactor
        //    DataTable dtSubFactor = new(Name);
        //    dtSubFactor.Columns.Add(new DataColumn("SL.No.", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("StageId", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("RiskTypeId", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("FactorId", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("SubFactorID", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("Stage", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("RiskType", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("Factor", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("SubFactor", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("Description", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("RiskWeightage", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("Response", typeof(string)));
        //    dtSubFactor.Columns.Add(new DataColumn("Comments", typeof(string)));
        //    var iIndex = 1;
        //    foreach (var subFactor in model.RiskSubFactors)
        //    {
        //        var factor = model.RiskFactors.FirstOrDefault(x => x.Id == subFactor.RiskFactorId);
        //        if (factor?.Name == Name)
        //        {
        //            DataRow dr = dtSubFactor.NewRow();
        //            dr["SL.No."] = iIndex;
        //            dr["RiskTypeId"] = model.RiskType?.Id;
        //            dr["StageId"] = model.Stage?.Id;
        //            dr["FactorId"] = factor?.Id;
        //            dr["SubFactorId"] = subFactor.Id;
        //            dr["RiskType"] = model.RiskType?.Name;
        //            dr["Stage"] = model.Stage?.Name;
        //            dr["Factor"] = factor?.Name;
        //            dr["SubFactor"] = subFactor.Name;
        //            dr["Description"] = subFactor.Description;
        //            if (subFactor.IsExcludedInRisk)
        //            {
        //                //dr["Low"] = "NA";
        //                //dr["Medium"] = "NA";
        //                //dr["High"] = "NA";
        //                dr["RiskWeightage"] = "NA";
        //                dr["Response"] = "NA";
        //            }
        //            else
        //            {
        //                if (subFactor.RiskRangeParameter == Common.RiskRangeParameter.PercentRange)
        //                {
        //                    //dr["Low"] = $"{subFactor.Percentage2}%";
        //                    //dr["Medium"] = $"{subFactor.Percentage3} - {subFactor.Percentage4}%";
        //                    //dr["High"] = $"{subFactor.Percentage5}%";
        //                    dr["Response"] = subFactor.Response;
        //                }
        //                else if (subFactor.RiskRangeParameter == Common.RiskRangeParameter.PreDefinedParameters)
        //                {
        //                    //dr["Low"] = subFactor.PreDefinedRiskParameter1?.Name;
        //                    //dr["Medium"] = subFactor.PreDefinedRiskParameter2?.Name;
        //                    //dr["High"] = subFactor.PreDefinedRiskParameter3?.Name;
        //                    if (subFactor.PreDefinedParameterId == subFactor.PreDefinedParameter1Id)
        //                        dr["Response"] = subFactor.PreDefinedRiskParameter1?.Name;
        //                    else if (subFactor.PreDefinedParameterId == subFactor.PreDefinedParameter2Id)
        //                        dr["Response"] = subFactor.PreDefinedRiskParameter2?.Name;
        //                    else if (subFactor.PreDefinedParameterId == subFactor.PreDefinedParameter3Id)
        //                        dr["Response"] = subFactor.PreDefinedRiskParameter3?.Name;
        //                }
        //                else if (subFactor.RiskRangeParameter == Common.RiskRangeParameter.Descriptive)
        //                {
        //                    //dr["Low"] = subFactor.RiskDescription1;
        //                    //dr["Medium"] = subFactor.RiskDescription2;
        //                    //dr["High"] = subFactor.RiskDescription3;
        //                    if (subFactor.ResponseDescription == subFactor.RiskDescription1)
        //                        dr["Response"] = subFactor.RiskDescription1;
        //                    else if (subFactor.ResponseDescription == subFactor.RiskDescription2)
        //                        dr["Response"] = subFactor.RiskDescription2;
        //                    else if (subFactor.ResponseDescription == subFactor.RiskDescription3)
        //                        dr["Response"] = subFactor.RiskDescription3;
        //                }
        //                else if (subFactor.RiskRangeParameter == Common.RiskRangeParameter.Volume)
        //                {
        //                    //dr["Low"] = "-";
        //                    //dr["Medium"] = "-";
        //                    //dr["High"] = "-";
        //                    dr["Response"] = subFactor.Response;
        //                }
        //                dr["RiskWeightage"] = $"{subFactor.RiskWeightage}%";
        //            }
        //            dr["Comments"] = subFactor.Assumptions;

        //            dtSubFactor.Rows.Add(dr);

        //            iIndex += 1;
        //        }



        //    }

        //    //if (factor == null)
        //    //    return ds;

        //    //risk volume

        //    return dtSubFactor;
        //}
        private DataTable GetRiskExcelTemplateFactor(AssessmentPillsViewModel model, string Name, out List<int> lockedRows)
        {
            lockedRows = new();
            //Subfactor
            DataTable dtSubFactor = new(Name);
            dtSubFactor.Columns.Add(new DataColumn("SL.No.", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("RegisterType", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("StageId", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("RiskTypeId", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("GeographicPresenceId", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("CustomerSegmentId", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("BusinessSegmentId", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("FactorId", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("SubFactorId", typeof(int)));
            dtSubFactor.Columns.Add(new DataColumn("Stage", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Risk Type", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Geographic Presence", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Business Segment", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Sub Unit", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Factor", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Sub Factor", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Description", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Response", typeof(string)));
            dtSubFactor.Columns.Add(new DataColumn("Comments", typeof(string)));
            var iIndex = 1;
            foreach (var factor in model.RiskFactors)
            {
                var hasProduct = model.ProductServiceMappings.Any(x => x.RiskFactorId == factor.Id);
                var hasVolume = factor.RiskRangeParameter == RiskRangeParameter.Volume;

                foreach (var subFactor in model.RiskSubFactors.Where(t => t.RiskFactorId == factor.Id))
                {
                    DataRow dr = dtSubFactor.NewRow();
                    dr["SL.No."] = iIndex;
                    dr["RegisterType"] = 1;
                    dr["StageId"] = factor!.StageId;
                    dr["RiskTypeId"] = factor.RiskTypeId.HasValue ? factor?.RiskTypeId.Value : DBNull.Value;
                    dr["GeographicPresenceId"] = factor.GeographicPresenceId.HasValue ? factor?.GeographicPresenceId.Value : DBNull.Value;
                    dr["CustomerSegmentId"] = factor.CustomerSegmentId.HasValue ? factor?.CustomerSegmentId.Value : DBNull.Value;
                    dr["BusinessSegmentId"] = factor.BusinessSegmentId.HasValue ? factor?.BusinessSegmentId.Value : DBNull.Value;
                    dr["FactorId"] = subFactor.RiskFactorId;
                    dr["SubFactorId"] = subFactor.Id;
                    dr["Stage"] = model.Stage?.Name;
                    dr["Risk Type"] = model.RiskType?.Name;
                    dr["Geographic Presence"] = model.GeographicPresence?.CountryName;
                    dr["Business Segment"] = model.CustomerSegment?.Name;
                    dr["Sub Unit"] = model.BusinessSegment?.Name;
                    dr["Factor"] = factor?.Name;
                    dr["Sub Factor"] = subFactor.Name;
                    dr["Description"] = subFactor.Description;
                    if (hasProduct || hasVolume || subFactor.IsExcludedInRisk)
                    {
                        lockedRows.Add(iIndex);
                        dr["Response"] = "NA";
                    }
                    else
                    {
                        if (factor.RiskRangeParameter == RiskRangeParameter.PercentRange)
                        {
                            dr["Response"] = subFactor.Response;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.PreDefinedParameters)
                        {
                            dr["Response"] = subFactor.PreDefinedParameterId;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.Descriptive)
                        {
                            dr["Response"] = subFactor.ResponseDescription;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.NumberRange)
                        {
                            dr["Response"] = subFactor.Response;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.Volume)
                        {
                            dr["Response"] = subFactor.Response;
                        }
                        else if (factor.RiskRangeParameter == RiskRangeParameter.Scale)
                        {
                            dr["Response"] = subFactor.Response;
                        }
                    }
                    dr["Comments"] = subFactor.Assumptions;
                    dtSubFactor.Rows.Add(dr);
                    iIndex += 1;
                }
            }
            return dtSubFactor;
        }
        private List<DataTable> GetRiskExcelTemplateVolumeSubFactor(AssessmentPillsViewModel model, ScaleType scaleType)
        {
            List<DataTable> dtList = new();
            var list = (_geographyRiskManager.GetWithoutOrderAsync(GetUserCustomerId(), new[] { "Country" }, t => t.IsActive)).Result.OrderBy(t => t.Country!.Name);
            foreach (var factor in model.RiskFactors)
            {
                var sheetIndex = 1;
                if (factor.RiskRangeParameter == RiskRangeParameter.Volume)
                {
                    foreach (var subFactor in model.RiskSubFactors.Where(x => x.RiskFactorId == factor?.Id))
                    {
                        var iIndex = 1;
                        DataTable dtSubFactor = new();
                        dtSubFactor.TableName = "Appendix 2- GRA" + Convert.ToString(sheetIndex);
                        dtSubFactor.Columns.Add(new DataColumn("SL.No.", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("RegisterType", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("StageId", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("RiskTypeId", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("GeographicPresenceId", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("CustomerSegmentId", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("BusinessSegmentId", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("FactorId", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("SubFactorId", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("CountryId", typeof(int)));
                        dtSubFactor.Columns.Add(new DataColumn("Stage", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Risk Type", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Geographic Presence", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Business Segment", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Sub Unit", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Factor", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Sub Factor", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Country", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Country Rating", typeof(string)));
                        dtSubFactor.Columns.Add(new DataColumn("Volume", typeof(decimal)));

                        List<CountryVolumeResponseViewModel> countryList = new();
                        foreach (var vol in list)
                        {
                            countryList.Add(new()
                            {
                                Id = vol.Id,
                                Name = vol.Country?.Name,
                                RiskRating = vol.RiskRating
                            });
                        }
                        var volResp = model.VolumeMappings.Where(x => x.RiskFactorId == subFactor.RiskFactorId && x.RiskSubFactorId == subFactor.Id).FirstOrDefault();
                        if (volResp != null && !string.IsNullOrWhiteSpace(volResp?.Countries) && !string.IsNullOrWhiteSpace(volResp?.CountryWiseRating) && !string.IsNullOrWhiteSpace(volResp?.CountryWiseVolume))
                        {
                            List<int> cList = volResp.Countries.Split(',').Select(t => Convert.ToInt32(t)).ToList();
                            List<int> ratingList = volResp.CountryWiseRating.Split(',').Select(t => Convert.ToInt32(t)).ToList();
                            List<decimal> volumeList = volResp.CountryWiseVolume.Split(',').Select(t => Convert.ToDecimal(t)).ToList();
                            if (cList.Count == ratingList.Count && ratingList.Count == volumeList.Count)
                            {
                                for (int i = 0; i < cList.Count; i++)
                                {
                                    var country = countryList.Where(t => t.Id == cList[i]).FirstOrDefault();
                                    if (country != null)
                                    {
                                        country.RiskRating = (RiskRating)ratingList[i];
                                        country.Volume = volumeList[i];
                                    }
                                }
                            }
                        }
                        foreach (var cL in countryList)
                        {
                            DataRow dr = dtSubFactor.NewRow();
                            dr["SL.No."] = iIndex;
                            dr["RegisterType"] = 2;
                            dr["StageId"] = factor!.StageId;
                            dr["RiskTypeId"] = factor.RiskTypeId.HasValue ? factor?.RiskTypeId.Value : DBNull.Value;
                            dr["GeographicPresenceId"] = factor.GeographicPresenceId.HasValue ? factor?.GeographicPresenceId.Value : DBNull.Value;
                            dr["CustomerSegmentId"] = factor.CustomerSegmentId.HasValue ? factor?.CustomerSegmentId.Value : DBNull.Value;
                            dr["BusinessSegmentId"] = factor.BusinessSegmentId.HasValue ? factor?.BusinessSegmentId.Value : DBNull.Value;
                            dr["FactorId"] = subFactor.RiskFactorId;
                            dr["SubFactorId"] = subFactor.Id;
                            dr["CountryId"] = cL?.Id;
                            dr["Stage"] = model.Stage?.Name;
                            dr["Risk Type"] = model.RiskType?.Name;
                            dr["Geographic Presence"] = model.GeographicPresence?.CountryName;
                            dr["Business Segment"] = model.CustomerSegment?.Name;
                            dr["Sub Unit"] = model.BusinessSegment?.Name;
                            dr["Factor"] = factor?.Name;
                            dr["Sub Factor"] = subFactor.Name;
                            dr["Country"] = cL.Name;
                            dr["Country Rating"] = Utilitiy.GetRatingText(scaleType, (int)cL.RiskRating);
                            dr["Volume"] = cL.Volume.HasValue ? cL.Volume : DBNull.Value;
                            dtSubFactor.Rows.Add(dr);
                            iIndex += 1;
                        }
                        sheetIndex += 1;
                        dtList.Add(dtSubFactor);
                    }
                    return dtList;
                }
            }
            return dtList;
        }
        private DataTable? GetRiskExcelTemplateProductSubFactor(AssessmentPillsViewModel model)
        {
            List<int> riskFactorIds = model.RiskFactors.Select(t => t.Id).ToList();

            if (!model.ProductServiceMappings.Any(x => riskFactorIds.Contains(x.RiskFactorId)))
            {
                return null;
            }
            var questionList = _riskAssessmentManager.GetQuestionRiskCriteriaMapping(GetUserCustomerId(), riskFactorIds).Result;
            DataTable dtProduct = new() { TableName = $"Appendix 1 - PRA" };
            dtProduct.Columns.Add(new DataColumn("SL.No.", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("RegisterType", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("StageId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("RiskTypeId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("GeographicPresenceId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("CustomerSegmentId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("BusinessSegmentId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("FactorId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("SubFactorId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("ProductId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("CriteriaId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("QuestionId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("Stage", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Risk Type", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Geographic Presence", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Business Segment", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Sub Unit", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Factor", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Sub Factor", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Product/ServiceCategory", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Value", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Criteria", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Question", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Purpose", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Answer", typeof(string)));
            var iIndex = 1;
            foreach (var factor in model.RiskFactors)
            {
                //var subFactors=model.RiskSubFactors.Where(t=>t.RiskFactorId==factor.Id).ToList();
                //foreach (var subFactor in subFactors)
                //{



                foreach (var product in model.ProductServiceMappings.Where(x => x.RiskFactorId == factor?.Id))
                {
                    foreach (var criteria in model.RiskCriteriaMappings.Where(t => t.RiskFactorId == product?.RiskFactorId && t.ProductId == product.ProductId))
                    {
                        List<int> answerQuestions = new();
                        List<int> answerAnswers = new();
                        if (!string.IsNullOrWhiteSpace(criteria.QuestionIds) && !string.IsNullOrWhiteSpace(criteria.Answers))
                        {
                            answerQuestions = criteria.QuestionIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(x => Convert.ToInt32(x.Trim())).ToList();
                            answerAnswers = criteria.Answers.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(x => Convert.ToInt32(x.Trim())).ToList();
                        }
                        foreach (var question in questionList.Where(t => t.RiskFactorId == factor.Id && t.RiskSubFactorId == product.RiskSubFactorId
                         && t.ProductId == product.ProductId && t.RiskCriteriaId == criteria.RiskCriteriaId))
                        {
                            DataRow dr = dtProduct.NewRow();
                            dr["SL.No."] = iIndex;
                            dr["RegisterType"] = 3;
                            dr["StageId"] = factor!.StageId;
                            dr["RiskTypeId"] = factor.RiskTypeId.HasValue ? factor?.RiskTypeId.Value : DBNull.Value;
                            dr["GeographicPresenceId"] = factor.GeographicPresenceId.HasValue ? factor?.GeographicPresenceId.Value : DBNull.Value;
                            dr["CustomerSegmentId"] = factor.CustomerSegmentId.HasValue ? factor?.CustomerSegmentId.Value : DBNull.Value;
                            dr["BusinessSegmentId"] = factor.BusinessSegmentId.HasValue ? factor?.BusinessSegmentId.Value : DBNull.Value;
                            dr["FactorId"] = product.RiskFactorId;
                            dr["SubFactorId"] = product.RiskSubFactorId;
                            dr["ProductId"] = product?.ProductId;
                            dr["CriteriaId"] = criteria.RiskCriteriaId;
                            dr["QuestionId"] = question.QuestionId;
                            dr["Stage"] = model.Stage?.Name;
                            dr["Risk Type"] = model.RiskType?.Name;
                            dr["Geographic Presence"] = model.GeographicPresence?.CountryName;
                            dr["Business Segment"] = model.CustomerSegment?.Name;
                            dr["Sub Unit"] = model.BusinessSegment?.Name;
                            dr["Factor"] = factor?.Name;
                            dr["Sub Factor"] = model?.RiskSubFactors?.Where(x => x.Id == product.RiskSubFactorId).FirstOrDefault()?.Name;
                            dr["Product/ServiceCategory"] = product?.ProductServiceName;
                            dr["Value"] = product?.Volume;
                            dr["Criteria"] = criteria?.RiskCriteria?.Name;
                            dr["Question"] = question.Questions?.Name;
                            dr["Purpose"] = question.Questions?.Description;
                            dr["Answer"] = answerQuestions.Contains(question.QuestionId) ? answerAnswers[answerQuestions.IndexOf(question.QuestionId)] : DBNull.Value;
                            dtProduct.Rows.Add(dr);
                            iIndex += 1;
                        }
                    }


                }
                //}
            }
            return dtProduct;
        }
        private string GetExcelFileName(AssessmentPillsViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.BusinessSegment?.Name))
                return model.BusinessSegment.Name;
            if (!string.IsNullOrWhiteSpace(model.CustomerSegment?.Name))
                return model.CustomerSegment.Name;
            if (!string.IsNullOrWhiteSpace(model.GeographicPresence?.CountryName))
                return model.GeographicPresence.CountryName;
            if (!string.IsNullOrWhiteSpace(model.RiskType?.Name))
                return model.RiskType.Name;
            if (!string.IsNullOrWhiteSpace(model.Stage?.Name))
                return model.Stage.Name;
            return string.Empty;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAssesment(AssessmentPillsViewModel model, int stageId, int? riskTypeId
            , int? geoPresenceId, int? customerSegmentId, int? businessSegmentId)
        {
            if (model == null || model.File == null)
                return Json(-3);

            if (model.File.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" && model.File.ContentType != "application/vnd.ms-excel")
            {
                return Json(-1);
            }

            ExcelReaderUtility excelReaderUtility = new();
            var customerId = GetUserCustomerId();
            var scaletype = (ScaleType)GetUserCustomerScale();

            var ds = excelReaderUtility.ReadExcelDs(model.File.OpenReadStream());
            AssessmentPillsViewModel assesmentModel = await GetRiskResponseModelsPills(stageId, riskTypeId
            , geoPresenceId, customerSegmentId, businessSegmentId);

            var countryList = (await _geographyRiskManager.GetWithoutOrderAsync(customerId, null, t => t.IsActive)).ToList();
            var result = await _riskAssessmentManager.ProcessRiskRegisterExcel(customerId, scaletype, ds, countryList, assesmentModel.ProductServiceMappings,
                assesmentModel.RiskSubFactors, assesmentModel.RiskFactors, GetUserId());
            return Json(result);
        }

        [HttpPost]
        public void SubFactorTempFileAdd(RiskSubFactorAttachmentViewModel model)
        {
            if (model.FormFile != null && model.RiskSubFactorId > 0)
            {
                foreach (var item in model.FormFile)
                {
                    FileUtility fileUtility = new(_storageSettings);
                    var attachmentModel = fileUtility.UploadFile(item);
                    model.FilePath = attachmentModel.FilePath;
                    model.FileName = attachmentModel.FileName;
                    model.CustomerId = GetUserCustomerId();
                    model.CreatedBy = GetUserId();
                    model.CreatedOn = DateTime.Now;
                    model.IsActive = true;
                    _riskAssessmentManager.SubFactorTempFileAdd(model);
                }

            }

        }

        [HttpPost]
        public async Task<IActionResult> SubmitApprovalRequest(ApprovalRequestViewModel modeldata, int PendingWithUserType)
        {
            if (modeldata == null || (!modeldata.ApprovalHistory.Any()))
            {
                return Json(new AppResultViewModel { Status = false, Message = "" });
            }


            modeldata.PendingFrom = DateTime.Now;
            modeldata.CustomerId = GetUserCustomerId();

            modeldata.Status = modeldata.ApprovalHistory[0].Status;
            modeldata.FinalStatus = modeldata.ApprovalHistory[0].Status;
            modeldata.IsActive = true;
            modeldata.CreatedBy = GetUserId();
            modeldata.CreatedOn = DateTime.Now;
            modeldata.PendingWithUserType = PendingWithUserType;
            foreach (var item in modeldata.ApprovalHistory)
            {
                item.CustomerId = modeldata.CustomerId;
                item.IsActive = true;
                item.CreatedBy = GetUserId();
                item.CreatedOn = DateTime.Now;
            }
            var result = await _riskAssessmentManager.SubmitApprovalRequest(modeldata, this.GetUserId());
            return Json(new AppResultViewModel { Status = result, Message = result ? "Responses submitted successfully" : "Something went wrong!" });
        }

        [HttpPost]
        public async Task<IActionResult> SaveApprovedResponse(int stageId, int? riskTypeId, int? geoPresenceId, int? customerSegmentId, int? businessSegmentId, string? riskFactor)
        {
            var customerId = GetUserCustomerId();
            var versionMaster = new CustomerVersionMaster();
            var isVersionCreation = _riskAssessmentManager.GetApprovalCompletion(customerId);
            if (!isVersionCreation)
            {
                return Json(new AppResultViewModel { Status = isVersionCreation, Message = "Responses submitted successfully" });
            }
            if (isVersionCreation)
            {
                versionMaster.CustomerId = customerId;
                versionMaster.IsActive = true;
                versionMaster.CreatedBy = GetUserCustomerId();
                versionMaster.CreatedOn = DateTime.Now;
            }

            List<int> riskFactorIds = new();
            List<int> risksubFactorIds = new();
            var riskFactors = await _riskFactorManager.GetAsync(customerId, null, null);

            //For single risk factor update
            if (int.TryParse(riskFactor?.Decrypt(), out int riskFactorId) && riskFactorId > 0)
                riskFactors = riskFactors.Where(t => t.Id == riskFactorId).ToList();

            if (riskFactors != null && riskFactors.Any())
            {
                riskFactorIds = riskFactors.Select(t => t.Id).ToList();
            }
            var subFactorResponses = await _riskAssessmentManager.GetRiskSubFactorResponse(customerId, riskFactorIds);
            if (subFactorResponses != null && subFactorResponses.Any())
            {
                risksubFactorIds = subFactorResponses.Select(t => t.Id).ToList();
            }

            var result = await _riskAssessmentManager.SaveApprovedResponse(customerId, riskFactorIds, risksubFactorIds, versionMaster);
            return Json(new AppResultViewModel { Status = result, Message = result ? "Responses submitted successfully" : "Something went wrong!" });
        }
        public async Task<IActionResult> SendEmail(string toemail, string subject, string htmlMessage)
        {
            string host = _EmailSettings.Host;
            int port = _EmailSettings.Port;
            string fromAddress = _EmailSettings.From;
            string userName = _EmailSettings.Username;
            string password = _EmailSettings.Password;
            var result = false;

            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.Body = htmlMessage;
            message.IsBodyHtml = true;
            message.To.Add(toemail);
            message.From = new MailAddress(fromAddress);
            try
            {
                using (var smtpClient = new SmtpClient(host, port))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(userName, password);
                    smtpClient.EnableSsl = true;
                    await smtpClient.SendMailAsync(message);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw;
            }
            return Json(new AppResultViewModel { Status = result, Message = result ? "Mail sent successfully" : "Something went wrong!" });
        }
    }
}
