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
    [Table(nameof(ApprovedRiskFactorResponse))]
    public class ApprovedRiskFactorResponse : BaseCustomerModel
    {
        public int RiskFactorId { get; set; }
        [Decimal]
        public decimal TotalWeightedScore { get; set; }
        [ForeignKey(nameof(RiskFactorId))]
        public RiskFactor? RiskFactor { get; set; }
        public int? ApprovalId { get; set; }
    }
}
