using FCRA.ViewModels.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.ViewModels.Base;

namespace FCRA.ViewModels.Responses
{
    public class ApprovedRiskFactorResponseViewModel : BaseCustomerViewModel
    {
        public int RiskFactorId { get; set; }
        public decimal TotalWeightedScore { get; set; }
        public RiskFactorViewModel? RiskFactor { get; set; }
        public int? ApprovalId { get; set; }
    }
}
