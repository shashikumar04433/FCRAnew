using FCRA.Common;
using FCRA.Models.Base;
using FCRA.Models.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Masters
{
    public class ApprovalRequest : BaseCustomerModel
    {
        public int StageId { get; set; }
        public int? RiskTypeId { get; set; }
        public int? GeographicPresenceId { get; set; }
        public int? CustomerSegmentId { get; set; }
        public int? BusinessSegmentId { get; set; }
        public int Status { get; set; }
        public int FinalStatus { get; set; }
        public int PendingWithUserType { get; set; } 
        public int PendingWithUser { get; set; }
        public int Sequence { get; set; }
        public DateTime? PendingFrom { get; set; }
        public string? VersionName { get; set; }
    }
}
