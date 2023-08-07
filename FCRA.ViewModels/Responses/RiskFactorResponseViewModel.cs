using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Masters;

namespace FCRA.ViewModels.Responses
{
    public class RiskFactorResponseViewModel : BaseCustomerViewModel
    {
        public int RiskFactorId { get; set; }
        public decimal TotalWeightedScore { get; set; }
        public RiskFactorViewModel? RiskFactor { get; set; }
        public int? ApprovalId { get; set; }
    }
}
