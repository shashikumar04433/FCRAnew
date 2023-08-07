using FCRA.Common;
using FCRA.Models.Base;
using FCRA.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Responses
{
    [Table(nameof(RiskScoreProductVolumRatingResponse))]
    public class RiskScoreProductVolumRatingResponse : BaseCustomerModel
    {
        [NotMapped]
        public override int Id { get; set; }

        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }
        public int ProductId { get; set; }

        [Decimal]
        public decimal? Volume { get; set; }
        public int? TotalScore { get; set; }
        public int? FinalScore { get; set; }
        public int RiskRating { get; set; }//1=Low, 2=Medium, 3=High

        [ForeignKey(nameof(RiskFactorId))]
        public RiskFactor? RiskFactor { get; set; }
        [ForeignKey(nameof(RiskSubFactorId))]
        public RiskSubFactor? RiskSubFactor { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductService? ProductService { get; set; }
        [Decimal]
        public decimal? Values { get; set; }
        [Decimal]
        public decimal? Value { get; set; }
    }
}
