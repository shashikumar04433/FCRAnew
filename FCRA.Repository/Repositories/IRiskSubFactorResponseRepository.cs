using FCRA.Models.Responses;
using FCRA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories
{
    internal interface IRiskSubFactorResponseRepository : IBaseModelCustomerRepository<RiskSubFactorResponse>
    {
        Task<List<RiskSubFactorResponse>> GetRiskSubFactorResponse(int customerId, List<int> riskFactorIds);
        Task<List<ApprovedRiskSubFactorResponse>> GetApprovedRiskSubFactorResponse(int customerId, int versionId, List<int> riskFactorIds);
    }
}
