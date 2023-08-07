using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.Models.Base;
using FCRA.Models.Masters;

namespace FCRA.Models.Responses
{
    [Table(nameof(ApprovedRiskScoreResponse))]
    public class ApprovedRiskScoreResponse : BaseCustomerModel
    {
        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }
        public int ProductId { get; set; }
        public int RiskCriteriaId { get; set; }
        public int Score { get; set; }
        [StringLength(200)]
        public string? QuestionIds { get; set; }
        [StringLength(200)]
        public string? Answers { get; set; }

        [ForeignKey(nameof(RiskFactorId))]
        public RiskFactor? RiskFactor { get; set; }

        [ForeignKey(nameof(RiskSubFactorId))]
        public RiskSubFactor? RiskSubFactor { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductService? ProductService { get; set; }
        [ForeignKey(nameof(RiskCriteriaId))]
        public RiskCriteria? RiskCriteria { get; set; }
        public int? ApprovalId { get; set; }
    }
}
