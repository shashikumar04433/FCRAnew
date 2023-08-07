using FCRA.Common;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace FCRA.ViewModels.Masters
{
    public class StageViewModel : BaseMasterCustomerViewModel
    {
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
        [NotMapped]
        public ScaleType ScaleType { get; set; }
        [NotMapped]
        public virtual List<RiskTypeViewModel> RiskTypes { get; set; } = new();
    }
}
