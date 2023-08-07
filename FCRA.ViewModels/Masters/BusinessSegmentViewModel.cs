using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FCRA.Common;
using FCRA.ViewModels.Base;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace FCRA.ViewModels.Masters
{
    public class BusinessSegmentViewModel : BaseMasterCustomerViewModel
    {
        [MapToDTO, Display(Name = "Business Segment")]
        public int CustomerSegmentId { get; set; }
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
        [NotMapped]
        public virtual CustomerSegmentViewModel? CustomerSegment { get; set; }
        public virtual List<RiskFactorViewModel>? RiskFactors { get; set; }
        [NotMapped]
        public ScaleType ScaleType { get; set; } = ScaleType.ThreePoint;
        [NotMapped, Required, Display(Name = "Stage")]
        public int? StageId { get; set; }
        [NotMapped, Required, Display(Name = "Risk Type")]
        public int? RiskTypeId { get; set; }
        [NotMapped, Required, Display(Name = "Geographic Presence")]
        public int? GeographicPresenceId { get; set; }
    }
}
