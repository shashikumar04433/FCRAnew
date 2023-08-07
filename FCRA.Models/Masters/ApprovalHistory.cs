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
    public class ApprovalHistory : BaseCustomerModel
    {
        public int ApprovalId { get; set; }
        public int Status { get; set; }
        public int Sequence { get; set; }
        public int PendingWithUser { get; set; }
        public string? Remark { get; set; }
        [ForeignKey(nameof(ApprovalId))]
        public virtual ApprovalRequest? ApprovalRequests { get; set; }
    }
}
