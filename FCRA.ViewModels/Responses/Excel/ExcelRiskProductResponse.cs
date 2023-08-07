using FCRA.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FCRA.ViewModels.Responses.Excel
{
    public class ExcelRiskProductResponse
    {
        public int StageId { get; set; }
        public int? RiskTypeId { get; set; }
        public int? GeographicPresenceId { get; set; }
        public int? CustomerSegmentId { get; set; }
        public int? BusinessSegmentId { get; set; }
        public int FactorId { get; set; }
        public int SubFactorId { get; set; }
        public int ProductId { get; set; }
        public int CriteriaId { get; set; }
        public int QuestionId { get; set; }
        public decimal? Value { get; set; }
        public int? Answer { get; set; }
    }
}
