using FCRA.Common;
using FCRA.ViewModels.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace FCRA.ViewModels.Masters
{
    public class RiskFactorViewModel : BaseMasterCustomerViewModel
    {
        [MapToDTO, Display(Name = "Stage")]
        public int StageId { get; set; }
        [MapToDTO, Display(Name = "Risk Type")]
        public int? RiskTypeId { get; set; }
        [MapToDTO, Display(Name = "Geographic Presence")]
        public int? GeographicPresenceId { get; set; }
        [MapToDTO, Display(Name = "Business Segment")]
        public int? CustomerSegmentId { get; set; }
        [MapToDTO, Display(Name = "Sub Unit")]
        public int? BusinessSegmentId { get; set; }

        [MapToDTO, DecimalNumber, Range(0, 100), Display(Name = "Weight%")]
        public decimal WeightPercentage { get; set; } = 10;
        [MapToDTO, DecimalNumber, Display(Name = "Scale Range 2")]
        public decimal ScaleRange2 { get; set; } = ScaleConstants.ScaleRange2;
        [MapToDTO, DecimalNumber, Display(Name = "Scale Range 3")]
        public decimal ScaleRange3 { get; set; } = ScaleConstants.ScaleRange3;
        [MapToDTO, DecimalNumber, RequiredIf($"{nameof(ScaleType)} == 4 || {nameof(ScaleType)} == 5"), Display(Name = "Scale Range 4")]
        public decimal? ScaleRange4 { get; set; } = ScaleConstants.ScaleRange4;
        [MapToDTO, DecimalNumber, RequiredIf($"{nameof(ScaleType)} == 5"), Display(Name = "Scale Range 5")]
        public decimal? ScaleRange5 { get; set; } = ScaleConstants.ScaleRange5;
        [MapToDTO, Display(Name = "Display Order")]
        public int Sequence { get; set; }
        [MapToDTO]
        [Display(Name = "Excluded In Risk Calculation")]
        public bool IsExcludedInRisk { get; set; }
        [MapToDTO, Display(Name = "Risk Range Parameter")]
        public RiskRangeParameter RiskRangeParameter { get; set; } = RiskRangeParameter.PercentRange;

        //Not mapped
        [NotMapped]
        public virtual StageViewModel? Stage { get; set; }

        [NotMapped]
        public virtual RiskTypeViewModel? RiskType { get; set; }
        [NotMapped]
        public virtual GeographicPresenceViewModel? GeographicPresence { get; set; }
        [NotMapped]
        public virtual CustomerSegmentViewModel? CustomerSegment { get; set; }
        [NotMapped]
        public virtual BusinessSegmentViewModel? BusinessSegment { get; set; }
        [NotMapped]
        public decimal TotalWeightedScore { get; set; }
        [NotMapped]
        public virtual List<RiskSubFactorViewModel> RiskSubFactors { get; set; } = new();
        [NotMapped]
        public ScaleType ScaleType { get; set; } = ScaleType.ThreePoint;
        [NotMapped]
        public decimal FromVersionTotalWeightedScore { get; set; }
        [NotMapped]
        public decimal ToVersionTotalWeightedScore { get; set; }
    }
}
