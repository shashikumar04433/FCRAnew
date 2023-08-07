using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using System.Data;

namespace FCRA.Repository.Repositories
{
    internal interface IRiskAssessmentRepository
    {
        Task<List<RiskSubFactor>> GetRiskSubFactorsByRiskType(int customerId, List<int> riskFactorIds);
        void SubFactorTempFileAdd(RiskSubFactorAttachment model);
        ApprovalRequest SubmitApprovalRequest(ApprovalRequest model);
        void SubmitApprovalRemark(ApprovalHistory model);
        Task<List<ApprovalRequest>> GetApprovalStatusData(int customerId, int stageId, int? riskTypeId, int? geoPresenceId, int? customerSegmentId, int? businessSegmentId);
        Task<List<CustomerVersionMaster>> GetApprovedVersion(int customerId);
        Task<List<UserMaster>> GetApproverList(int customerid);
        DataSet GetApprovedRiskCombination(int versionId);
    }
}
