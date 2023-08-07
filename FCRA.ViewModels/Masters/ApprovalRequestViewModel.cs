using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.Common;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Responses;

namespace FCRA.ViewModels.Masters
{
    public class ApprovalRequestViewModel : BaseCustomerViewModel
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
        public List<ApprovalHistoryViewModel> ApprovalHistory { get; set; }=new List<ApprovalHistoryViewModel>();
        public ApprovalStatusViewModel ApprovalStatus { get; set; } =new ApprovalStatusViewModel();
        public string? VersionName { get; set; }
        public List<RiskFactorComparisonViewModel> riskFactorComparisons { get; set;} =new List<RiskFactorComparisonViewModel>();
    }
}
