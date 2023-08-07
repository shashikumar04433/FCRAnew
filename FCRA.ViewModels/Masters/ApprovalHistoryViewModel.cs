using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.ViewModels.Base;

namespace FCRA.ViewModels.Masters
{
    public class ApprovalHistoryViewModel : BaseCustomerViewModel
    {
        public int ApprovalId { get; set; }
        public int Status { get; set; }
        public int Sequence { get; set; }
        public int PendingWithUser { get; set; }
        public string? Remark { get; set; }
    }
}
