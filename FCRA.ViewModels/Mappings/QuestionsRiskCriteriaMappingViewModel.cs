using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.Common;
using System.Xml.Linq;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Masters;

namespace FCRA.ViewModels.Mappings
{
    public class QuestionsRiskCriteriaMappingViewModel : BaseCustomerViewModel
    {
        public int RiskSubFactorId { get; set; }
        public int RiskFactorId { get; set; }
        public int ProductId { get; set; }
        public int RiskCriteriaId { get; set; }
        public int QuestionId { get; set; }
        public QuestionsViewModel? Questions { get; set; }
        [NotMapped]
        public int SelectedRating { get; set; }
    }
}
