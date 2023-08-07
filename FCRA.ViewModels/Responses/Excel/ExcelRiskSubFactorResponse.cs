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
    public class ExcelRiskSubFactorResponse
    {
        public int StageId { get; set; }
        public int? RiskTypeId { get; set; }
        public int? GeographicPresenceId { get; set; }
        public int? CustomerSegmentId { get; set; }
        public int? BusinessSegmentId { get; set; }
        public int FactorId { get; set; }
        public int SubFactorId { get; set; }
        public string? Response { get; set; }
        public string? Comments { get; set; }
    }
}
