using FCRA.Common;
using FCRA.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FCRA.Models.Masters
{
    [Table(nameof(RiskFactor))]
    public class RiskFactor : BaseMasterCustomerModel
    {
        [Required, Column(Order = 1)]
        public override string? Name { get; set; }

        [Column(Order = 2)]
        public override string? Description { get; set; }
        public int StageId { get; set; }
        public int? RiskTypeId { get; set; }
        public int? GeographicPresenceId { get; set; }
        public int? CustomerSegmentId { get; set; }
        public int? BusinessSegmentId { get; set; }
        [Decimal]
        public decimal WeightPercentage { get; set; } = 10;
        [Decimal]
        public decimal ScaleRange2 { get; set; } = ScaleConstants.ScaleRange2;
        [Decimal]
        public decimal ScaleRange3 { get; set; } = ScaleConstants.ScaleRange3;
        [Decimal]
        public decimal? ScaleRange4 { get; set; } = ScaleConstants.ScaleRange4;
        [Decimal]
        public decimal? ScaleRange5 { get; set; } = ScaleConstants.ScaleRange5;
        public int Sequence { get; set; }
        public bool IsExcludedInRisk { get; set; }
        public RiskRangeParameter RiskRangeParameter { get; set; }
        [ForeignKey(nameof(StageId))]
        public virtual Stage? Stage { get; set; }

        [ForeignKey(nameof(RiskTypeId))]
        public virtual RiskType? RiskType { get; set; }
        [ForeignKey(nameof(GeographicPresenceId))]
        public virtual GeographicPresence? GeographicPresence { get; set; }
        [ForeignKey(nameof(CustomerSegmentId))]
        public virtual CustomerSegment? CustomerSegment { get; set; }
        [ForeignKey(nameof(BusinessSegmentId))]
        public virtual BusinessSegment? BusinessSegment { get; set; }
        public virtual List<RiskSubFactor>? RiskSubFactors { get; set; }
    }
}
