using FCRA.Common;
using FCRA.Models.Base;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRA.Models
{
    [Table(nameof(RiskSubFactor))]

    public class RiskSubFactor : BaseMasterCustomerModel
    {
        [Required, Column(Order = 1), StringLength(-1)]
        public override string? Name { get; set; }

        [Required, Column(Order = 2), StringLength(-1)]
        public override string? Description { get; set; }
        public int RiskFactorId { get; set; }
        //Percent start
        [Decimal]
        public decimal? Percentage2 { get; set; }
        [Decimal]
        public decimal? Percentage3 { get; set; }
        [Decimal]
        public decimal? Percentage4 { get; set; }
        [Decimal]
        public decimal? Percentage5 { get; set; }
        //Percent end
        //Pre-defined start
        public int? PreDefinedParameter1Id { get; set; }
        public int? PreDefinedParameter2Id { get; set; }
        public int? PreDefinedParameter3Id { get; set; }
        public int? PreDefinedParameter4Id { get; set; }
        public int? PreDefinedParameter5Id { get; set; }
        //Pre-defined end
        //Descriptive start
        public string? RiskDescription1 { get; set; }
        public string? RiskDescription2 { get; set; }
        public string? RiskDescription3 { get; set; }
        public string? RiskDescription4 { get; set; }
        public string? RiskDescription5 { get; set; }
        //Descriptive end
        //Volume Start
        [Decimal]
        public decimal? RiskVolume1 { get; set; }
        [Decimal]
        public decimal? RiskVolume2 { get; set; }
        [Decimal]
        public decimal? RiskVolume3 { get; set; }
        [Decimal]
        public decimal? RiskVolume4 { get; set; }
        [Decimal]
        public decimal? RiskVolume5 { get; set; }
        //Volume End
        //Number Start
        [Decimal]
        public decimal? Number2 { get; set; }
        [Decimal]
        public decimal? Number3 { get; set; }
        [Decimal]
        public decimal? Number4 { get; set; }
        [Decimal]
        public decimal? Number5 { get; set; }
        //Number End
        [Decimal]
        public decimal? RiskWeightage { get; set; }
        public int Sequence { get; set; }
        [ForeignKey(nameof(RiskFactorId))]
        public virtual RiskFactor? RiskFactor { get; set; }

        [ForeignKey(nameof(PreDefinedParameter1Id))]
        public PreDefinedRiskParameter? PreDefinedRiskParameter1 { get; set; }
        [ForeignKey(nameof(PreDefinedParameter2Id))]
        public PreDefinedRiskParameter? PreDefinedRiskParameter2 { get; set; }
        [ForeignKey(nameof(PreDefinedParameter3Id))]
        public PreDefinedRiskParameter? PreDefinedRiskParameter3 { get; set; }
        [ForeignKey(nameof(PreDefinedParameter4Id))]
        public PreDefinedRiskParameter? PreDefinedRiskParameter4 { get; set; }
        [ForeignKey(nameof(PreDefinedParameter5Id))]
        public PreDefinedRiskParameter? PreDefinedRiskParameter5 { get; set; }
        public List<RiskSubFactorAttachment>? riskSubFactorAttachment { get; set; }
        public bool isAttachmentApplicable { get; set; } = false;
        public bool isAttachmentMandatory { get; set; } = false;
    }
}
