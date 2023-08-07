using FCRA.Common;
using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Responses
{
    public class RiskScoreProductVolumRatingResponseViewModel : BaseCustomerViewModel
    {
        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }
        public int ProductId { get; set; }

        [Decimal]
        public decimal? Volume { get; set; }
        public int? TotalScore { get; set; }
        public int? FinalScore { get; set; }
        public int RiskRating { get; set; } = 1;//1=Low, 2=Medium, 3=High
        [Decimal]
        public decimal? Value { get; set; }
        public RiskSubFactorAttachmentViewModel RiskSubFactorAttachments { get; set; } = new();
        public int? ApprovalId { get; set; }
    }
}
