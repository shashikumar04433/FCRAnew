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
    [Table(nameof(ApprovedRiskSubFactorResponse))]
    public class ApprovedRiskSubFactorResponse : BaseCustomerModel
    {

        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }

        public int Score { get; set; }
        public string? Assumptions { get; set; }

        [Decimal]
        public decimal? Response { get; set; }
        public int? PreDefinedParameterId { get; set; }
        public string? ResponseDescription { get; set; }
        public int? ScaleResponse { get; set; }

        [ForeignKey(nameof(RiskFactorId))]
        public RiskFactor? RiskFactor { get; set; }
        [ForeignKey(nameof(RiskSubFactorId))]
        public RiskSubFactor? RiskSubFactor { get; set; }
        [ForeignKey(nameof(PreDefinedParameterId))]
        public PreDefinedRiskParameter? PreDefinedRiskParameter { get; set; }
        public int? ApprovalId { get; set; }
    }
}
