using FCRA.Common;
using FCRA.Models.Base;
using FCRA.Models.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRA.Models.Mappings
{
    [Table(nameof(RiskFactorProductServiceMapping))]
    public class RiskFactorProductServiceMapping : BaseCustomerModel
    {
        [NotMapped]
        public override int Id { get; set; }
        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }
        public int ProductId { get; set; }
        public int ScaleRange2 { get; set; } = ScaleConstantsInt.ScaleRange2;
        public int ScaleRange3 { get; set; } = ScaleConstantsInt.ScaleRange3;
        public int? ScaleRange4 { get; set; } 
        public int? ScaleRange5 { get; set; }


        [ForeignKey(nameof(RiskFactorId))]
        public virtual RiskFactor? RiskFactor { get; set; }
        [ForeignKey(nameof(RiskSubFactorId))]
        public virtual RiskSubFactor? RiskSubFactor { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual ProductService? ProductService { get; set; }
    }
}
