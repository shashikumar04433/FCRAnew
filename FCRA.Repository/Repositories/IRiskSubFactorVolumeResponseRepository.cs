using FCRA.Models.Responses;
using FCRA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Repositories
{
    internal interface IRiskSubFactorVolumeResponseRepository : IBaseModelCustomerRepository<RiskSubFactorVolumeResponse>
    {
        Task<List<RiskSubFactorVolumeResponse>> GetRiskSubFactorVolumeResponse(int customerId, List<int> riskFactorIds);
        Task<List<ApprovedRiskSubFactorVolumeResponse>> GetApprovedRiskSubFactorVolumeResponse(int customerId, int versionId, List<int> riskFactorIds);
    }
}
