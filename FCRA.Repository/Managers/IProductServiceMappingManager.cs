using FCRA.ViewModels.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers
{
    public interface IProductServiceMappingManager : IModelManagerCustomer<RiskFactorProductServiceMappingViewModel>
    {
        Task<bool> UpdateProductServiceMappings(int customerId,List<RiskFactorProductServiceMappingViewModel> mappings, int riskFactorId, int riskSubFactorId, int userId);
    }
}
