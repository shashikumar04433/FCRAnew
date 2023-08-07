using System.ComponentModel.DataAnnotations;
using FCRA.ViewModels.Base;

namespace FCRA.ViewModels.Responses
{
    public class RiskSubFactorVolumeResponseViewModel : BaseCustomerViewModel
    {
        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }
        [Required, DecimalNumber(3, 2)]
        public decimal? Score1 { get; set; }
        [Required, DecimalNumber(3, 2)]
        public decimal? Score2 { get; set; }
        [Required, DecimalNumber(3, 2)]
        public decimal? Score3 { get; set; }
        [DecimalNumber(3, 2)]
        public decimal? Score4 { get; set; }
        [DecimalNumber(3, 2)]
        public decimal? Score5 { get; set; }
        [Required, DecimalNumber]
        public decimal? Volume1 { get; set; }
        [Required, DecimalNumber]
        public decimal? Volume2 { get; set; }
        [Required, DecimalNumber]
        public decimal? Volume3 { get; set; }
        [DecimalNumber]
        public decimal? Volume4 { get; set; }
        [DecimalNumber]
        public decimal? Volume5 { get; set; }
        [Required, DecimalNumber(3, 2)]
        public decimal? Weight1 { get; set; }
        [Required, DecimalNumber(3, 2)]
        public decimal? Weight2 { get; set; }
        [Required, DecimalNumber(3, 2)]
        public decimal? Weight3 { get; set; }
        [DecimalNumber(3, 2)]
        public decimal? Weight4 { get; set; }
        [DecimalNumber(3, 2)]
        public decimal? Weight5 { get; set; }
        [Required, DecimalNumber(3, 2)]
        public decimal? WeightedScore1 { get; set; }
        [Required, DecimalNumber(3, 2)]
        public decimal? WeightedScore2 { get; set; }
        [Required, DecimalNumber(3, 2)]
        public decimal? WeightedScore3 { get; set; }
        [DecimalNumber(3, 2)]
        public decimal? WeightedScore4 { get; set; }
        [DecimalNumber(3, 2)]
        public decimal? WeightedScore5 { get; set; }
        public string? Countries { get; set; }
        public string? CountryWiseRating { get; set; }
        public string? CountryWiseVolume { get; set; }
        public int? ApprovalId { get; set; }
    }
}
