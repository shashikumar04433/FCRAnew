using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories
{
    internal interface IRiskFactorResponseRepository : IBaseModelCustomerRepository<RiskFactorResponse>
    {
        Task<List<RiskFactorResponse>> GetRiskFactorResponse(int customerId, List<int> riskFactorIds);
        Task<bool> SubmitApprovedRiskFactorResponses(int customerId, int approvalId, List<int> riskFactorIds, List<int> risksubFactorIds);
        Task<List<ApprovedRiskFactorResponse>> GetVersionRiskFactorResponse(int customerId, List<int> riskFactorIds, int VersionId);
        DataSet GetApprovalCompletion(int customerId);
        int SubmitVersionMaster(CustomerVersionMaster model);
        Task<List<ApprovedRiskFactorResponse>> GetApprovedRiskFactorResponse(int customerId, int versionId, List<int> riskFactorIds);
    }
}
