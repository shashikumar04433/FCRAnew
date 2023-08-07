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
    public class ApprovalMatrix : BaseCustomerModel
    {
        public int StageId { get; set; }
        public int? RiskTypeId { get; set; }
        public int? GeographicPresenceId { get; set; }
        public int? CustomerSegmentId { get; set; }
        public int? BusinessSegmentId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int SequenceNo { get; set; }
    }
}
