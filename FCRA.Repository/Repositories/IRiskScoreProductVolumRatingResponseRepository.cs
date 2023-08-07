using FCRA.Models.Responses;
using FCRA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories
{
    internal interface IRiskScoreProductVolumRatingResponseRepository : IBaseModelCustomerRepository<RiskScoreProductVolumRatingResponse>
    {
        Task<List<RiskScoreProductVolumRatingResponse>> GetRiskScoreProductVolumRatingResponse(int customerId, List<int> riskFactorIds);
        Task<List<ApprovedRiskScoreProductVolumRatingResponse>> GetApprovedRiskScoreProductVolumRatingResponse(int customerId, int versionId, List<int> riskFactorIds);
    }
}
