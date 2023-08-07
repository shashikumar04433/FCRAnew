using System.ComponentModel.DataAnnotations;
using FCRA.Common;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Masters;

namespace FCRA.ViewModels.Responses
{
    public class RiskSubFactorResponseViewModel : BaseCustomerViewModel
    {
        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }

        public RiskRangeParameter RiskRangeParameter { get; set; }
        [Required]
        public int? Score { get; set; }
        public string? Assumptions { get; set; }

        [DecimalNumber]
        public decimal? Response { get; set; }
        public int? PreDefinedParameterId { get; set; }
        public string? ResponseDescription { get; set; }

        public RiskFactorViewModel? RiskFactor { get; set; }
        public RiskSubFactorViewModel? RiskSubFactor { get; set; }
        public PreDefinedRiskParameterViewModel? PreDefinedRiskParameter { get; set; }
        public int? ApprovalId { get; set; }
    }
}
