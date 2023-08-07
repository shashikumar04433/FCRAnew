using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FCRA.Common;
using System.Xml.Linq;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Masters;

namespace FCRA.ViewModels.Mappings
{
    public class ProductRiskCriteriaMappingViewModel : BaseCustomerViewModel
    {
        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }
        public int ProductId { get; set; }
        public int RiskCriteriaId { get; set; }
        public int Score { get; set; }

        public RiskFactorViewModel? RiskFactor { get; set; }
        public RiskSubFactorViewModel? RiskSubFactor { get; set; }
        public ProductServiceViewModel? ProductService { get; set; }
        public RiskCriteriaViewModel? RiskCriteria { get; set; }


        [NotMapped]
        public bool IsSelected { get; set; }
        [NotMapped]
        public string? RiskCriteriaName { get; set; }
        [NotMapped]
        public string? QuestionIds { get; set; }
    }
}
