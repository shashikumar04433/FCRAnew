using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.ViewModels.Base;

namespace FCRA.ViewModels.Masters
{
    public class ApprovalMatrixViewModel: BaseCustomerViewModel
    {
        [Display(Name ="Stage")]
        public int StageId { get; set; }
        [Display(Name = "Risk Type")]
        public int? RiskTypeId { get; set; }
        [Display(Name = "Geographic Presence")]
        public int? GeographicPresenceId { get; set; }
        [Display(Name = "Business Segment")]
        public int? CustomerSegmentId { get; set; }
        [Display(Name = "Sub Unit")]
        public int? BusinessSegmentId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int SequenceNo { get; set; }
        public string? UserName { get; set; }
    }
}
