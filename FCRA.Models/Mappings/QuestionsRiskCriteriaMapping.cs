using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.Models.Base;
using FCRA.Models.Masters;

namespace FCRA.Models.Mappings
{
    [Table(nameof(QuestionsRiskCriteriaMapping))]
    public class QuestionsRiskCriteriaMapping : BaseCustomerModel
    {
        [NotMapped]
        public override int Id { get; set; }

        public int RiskSubFactorId { get; set; }
        public int RiskFactorId { get; set; }
        public int ProductId { get; set; }
        public int RiskCriteriaId { get; set; }
        public int QuestionId { get; set; }

        [ForeignKey(nameof(RiskFactorId))]
        public RiskFactor? RiskFactor { get; set; }
        [ForeignKey(nameof(RiskSubFactorId))]
        public virtual RiskSubFactor? RiskSubFactor { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductService? ProductService { get; set; }
        [ForeignKey(nameof(RiskCriteriaId))]
        public RiskCriteria? RiskCriteria { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public Questions? Questions { get; set; }
    }
}
