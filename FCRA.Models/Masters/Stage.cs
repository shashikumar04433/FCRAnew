using FCRA.Common;
using FCRA.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Masters
{
    public class Stage : BaseMasterCustomerModel
    {
        [Decimal]
        public decimal ScaleRange2 { get; set; } = ScaleConstants.ScaleRange2;
        [Decimal]
        public decimal ScaleRange3 { get; set; } = ScaleConstants.ScaleRange3;
        [Decimal]
        public decimal? ScaleRange4 { get; set; } = ScaleConstants.ScaleRange4;
        [Decimal]
        public decimal? ScaleRange5 { get; set; } = ScaleConstants.ScaleRange5;
        public int Sequence { get; set; }
        public bool ExcludeChildCategory { get; set; }
        public virtual List<RiskType>? RiskTypes { get; set; }
    }
}
