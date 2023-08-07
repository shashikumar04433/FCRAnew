using System.ComponentModel.DataAnnotations.Schema;
using FCRA.Models.Base;
using FCRA.Models.Masters;

namespace FCRA.Models.Mappings
{
    [Table(nameof(ProductRiskCriteriaMapping))]
    public class ProductRiskCriteriaMapping : BaseCustomerModel
    {
        [NotMapped]
        public override int Id { get; set; }
        public int RiskSubFactorId { get; set; }
        public int RiskFactorId { get; set; }
        public int ProductId { get; set; }
        public int RiskCriteriaId { get; set; }

        [ForeignKey(nameof(RiskFactorId))]
        public RiskFactor? RiskFactor { get; set; }
        [ForeignKey(nameof(RiskSubFactorId))]
        public virtual RiskSubFactor? RiskSubFactor { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductService? ProductService { get; set; }
        [ForeignKey(nameof(RiskCriteriaId))]
        public RiskCriteria? RiskCriteria { get; set; }
    }
}
