using FCRA.Common;
using FCRA.ViewModels.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using System.Text.Json.Serialization;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace FCRA.ViewModels.Masters
{
    public class RiskTypeViewModel : BaseMasterCustomerViewModel
    {
        [MapToDTO, Display(Name = "Stage")]
        public int StageId { get; set; }
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
        [MapToDTO, Display(Name = "Exclude Child Category")]
        public bool ExcludeChildCategory { get; set; }
        public virtual StageViewModel? Stage { get; set; }
        [NotMapped]
        public virtual List<GeographicPresenceViewModel> GeographicPresences { get; set; } = new();
        [NotMapped]
        public ScaleType ScaleType { get; set; } = ScaleType.ThreePoint;
    }
}
