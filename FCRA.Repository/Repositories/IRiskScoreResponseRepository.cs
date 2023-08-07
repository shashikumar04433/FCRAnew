using FCRA.Models.Responses;
using FCRA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories
{
    internal interface IRiskScoreResponseRepository : IBaseModelCustomerRepository<RiskScoreResponse>
    {
        Task<List<RiskScoreResponse>> GetRiskScoreResponse(int customerId, List<int> riskFactorIds);
        Task<List<ApprovedRiskScoreResponse>> GetApprovedRiskScoreResponse(int customerId, int VersionId, List<int> riskFactorIds);
    }
}
