using FCRA.Common;
using FCRA.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Masters
{
    [Table(nameof(BusinessSegment))]
    public class BusinessSegment : BaseMasterCustomerModel
    {
        public int CustomerSegmentId { get; set; }
        [Decimal]
        public decimal ScaleRange2 { get; set; } = ScaleConstants.ScaleRange2;
        [Decimal]
        public decimal ScaleRange3 { get; set; } = ScaleConstants.ScaleRange3;
        [Decimal]
        public decimal? ScaleRange4 { get; set; } = ScaleConstants.ScaleRange4;
        [Decimal]
        public decimal? ScaleRange5 { get; set; } = ScaleConstants.ScaleRange5;
        public int Sequence { get; set; }
        [ForeignKey(nameof(CustomerSegmentId))]
        public virtual CustomerSegment? CustomerSegment { get; set; }
        public virtual List<RiskFactor>? RiskFactors { get; set; }
    }
}
