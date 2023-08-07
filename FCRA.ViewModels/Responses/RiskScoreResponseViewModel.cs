using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Masters;

namespace FCRA.ViewModels.Responses
{
    public class RiskScoreResponseViewModel : BaseCustomerViewModel
    {
        public int RiskFactorId { get; set; }
        public int RiskSubFactorId { get; set; }
        public int ProductId { get; set; }
        public int RiskCriteriaId { get; set; }
        [Required]
        public int? Score { get; set; }
        public string? QuestionIds { get; set; }
        public string? Answers { get; set; }

        public RiskFactorViewModel? RiskFactor { get; set; }
        public RiskSubFactorViewModel? RiskSubFactor { get; set; }
        public ProductServiceViewModel? ProductService { get; set; }
        public RiskCriteriaViewModel? RiskCriteria { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
        [NotMapped]
        public string? RiskCriteriaName { get; set; }
        public int? ApprovalId { get; set; }
    }
}
