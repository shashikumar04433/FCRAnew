using FCRA.Common;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Mappings;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Responses;
using FCRA.ViewModels.Responses.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers
{
    public interface IRiskAssessmentManager
    {
        Task<List<RiskSubFactorViewModel>> GetRiskSubFactorsByRiskType(int customerId, List<int> riskFactorIds);
        Task<List<RiskScoreResponseViewModel>> GetRiskScoreResponse(int customerId, List<int> riskFactorIds);
        Task<List<RiskFactorResponseViewModel>> GetRiskFactorResponse(int customerId, List<int> riskFactorIds);
        Task<List<RiskSubFactorResponseViewModel>> GetRiskSubFactorResponse(int customerId, List<int> riskFactorIds);
        Task<List<RiskSubFactorVolumeResponseViewModel>> GetRiskSubFactorVolumeResponse(int customerId, List<int> riskFactorIds);
        Task<List<RiskScoreProductVolumRatingResponse>> GetRiskScoreProductVolumRatingResponse(int customerId, List<int> riskFactorIds);
        Task<List<QuestionsRiskCriteriaMappingViewModel>> GetQuestionRiskCriteriaMapping(int customerId, int riskFactorId, int riskSubFactorId, int productId, int riskCriteriaId);
        Task<List<QuestionsRiskCriteriaMappingViewModel>> GetQuestionRiskCriteriaMapping(int customerId, List<int> riskFactorIds);
        Task<bool> UpdateAssessmentResponse(int customerId, ResponseSaveViewModel responses, List<int> riskFactorIds, int userId);
        Task<int> ProcessRiskRegisterExcel(int customerId, ScaleType scaleType, DataSet ds, List<GeographyRiskViewModel> riskCountries, List<RiskFactorProductServiceMappingViewModel> productMappings, List<RiskSubFactorViewModel> riskSubFactors, List<RiskFactorViewModel> riskFactors, int userId);
        void SubFactorTempFileAdd(RiskSubFactorAttachmentViewModel model);
        Task<bool> SubmitApprovalRequest(ApprovalRequestViewModel modeldata, int userId);
        Task<List<ApprovalRequestViewModel>> GetApprovalStatusData(int customerId, int stageId, int? riskTypeId, int? geoPresenceId, int? customerSegmentId, int? businessSegmentId);
        Task<bool> SaveApprovedResponse(int customerId, List<int> riskFactorIds, List<int> risksubFactorIds, CustomerVersionMaster versionMaster);
        Task<List<CustomerVersionMasterViewModel>> GetApprovedVersion(int customerid);
        Task<List<UserViewModel>> GetApproverList(int customerid);
        Task<List<RiskFactorResponseViewModel>> GetApprovedRiskFactorResponse(int customerId, List<int> riskFactorIds, int VersionId);
        bool GetApprovalCompletion(int customerId);

        Task<List<RiskScoreResponseViewModel>> GetApprovedRiskScoreResponse(int customerId, int versionId, List<int> riskFactorIds);
        Task<List<RiskFactorResponseViewModel>> GetApprovedRiskFactorResponse(int customerId, int versionId, List<int> riskFactorIds);
        Task<List<RiskSubFactorResponseViewModel>> GetApprovedRiskSubFactorResponse(int customerId, int versionId, List<int> riskFactorIds);
        Task<List<RiskSubFactorVolumeResponseViewModel>> GetApprovedRiskSubFactorVolumeResponse(int customerId, int versionId, List<int> riskFactorIds);
        Task<List<RiskScoreProductVolumRatingResponseViewModel>> GetApprovedRiskScoreProductVolumRatingResponse(int customerId, int versionId, List<int> riskFactorIds);
        DataSet GetApprovedRiskCombination(int versionId);
    }
}
