using FCRA.Common;
using FCRA.Models.Base;
using FCRA.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Responses
{
    [Table(nameof(RiskSubFactorVolumeResponse))]
    public class RiskSubFactorVolumeResponse : BaseCustomerModel
    {
        [NotMapped]
        public override int Id { get; set; }

        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }
        [Required, Decimal]
        public decimal? Score1 { get; set; }
        [Required, Decimal]
        public decimal? Score2 { get; set; }
        [Required, Decimal]
        public decimal? Score3 { get; set; }
        [ Decimal]
        public decimal? Score4 { get; set; }
        [ Decimal]
        public decimal? Score5 { get; set; }
        [Required, Decimal]
        public decimal? Volume1 { get; set; }
        [Required, Decimal]
        public decimal? Volume2 { get; set; }
        [Required, Decimal]
        public decimal? Volume3 { get; set; }
        [Decimal]
        public decimal? Volume4 { get; set; }
        [Decimal]
        public decimal? Volume5 { get; set; }
        [Required, Decimal]
        public decimal? Weight1 { get; set; }
        [Required, Decimal]
        public decimal? Weight2 { get; set; }
        [Required, Decimal]
        public decimal? Weight3 { get; set; }
        [Decimal]
        public decimal? Weight4 { get; set; }
        [Decimal]
        public decimal? Weight5 { get; set; }
        [Required, Decimal]
        public decimal? WeightedScore1 { get; set; }
        [Required, Decimal]
        public decimal? WeightedScore2 { get; set; }
        [Required, Decimal]
        public decimal? WeightedScore3 { get; set; }
        [Decimal]
        public decimal? WeightedScore4 { get; set; }
        [Decimal]
        public decimal? WeightedScore5 { get; set; }

        [ForeignKey(nameof(RiskFactorId))]
        public RiskFactor? RiskFactor { get; set; }
        [ForeignKey(nameof(RiskSubFactorId))]
        public RiskSubFactor? RiskSubFactor { get; set; }
        public string? Countries { get; set; }
        public string? CountryWiseRating { get; set; }
        public string? CountryWiseVolume { get; set; }
    }
}
