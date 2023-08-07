using System.ComponentModel.DataAnnotations.Schema;
using FCRA.Models.Base;

namespace FCRA.Models.Masters
{
    [Table(nameof(RiskCriteria))]
    public class RiskCriteria : BaseMasterCustomerModel
    {
        public int Sequence { get; set; }
    }
}
