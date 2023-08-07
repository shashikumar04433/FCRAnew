using FCRA.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.ResponseVersion
{
    public class ResponseVersion : BaseCustomerModel
    {
        public int Status { get; set; }
        public int ReviewerStatus { get; set; }
        public DateTime? ReviewerActionOn { get; set; }
        public string? ReviewerActionBy { get; set; }
        public int ApproverStatus { get; set; }
        public DateTime? ApproverActionOn { get; set; }
        public string? ApproverActionBy { get; set; }

    }
}
