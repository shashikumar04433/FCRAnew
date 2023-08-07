using FCRA.ViewModels.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.Managers
{
    public interface IProductRiskCriteriaMappingManager : IModelManagerCustomer<ProductRiskCriteriaMappingViewModel>
    {
        Task<bool> UpdateProductRiskCriteriaMappings(int customerId, List<ProductRiskCriteriaMappingViewModel> mappings, int riskFactorId, int riskSubFactorId, int userId);
        Task<List<QuestionsRiskCriteriaMappingViewModel>> GetQuestionsRiskCriteriaMappings(int customerId, int riskFactorId, int riskSubFactorId);
    }
}
