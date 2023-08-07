using FCRA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Responses.Excel
{
    public class ExcelRiskVolumeResponse
    {
        public int StageId { get; set; }
        public int? RiskTypeId { get; set; }
        public int? GeographicPresenceId { get; set; }
        public int? CustomerSegmentId { get; set; }
        public int? BusinessSegmentId { get; set; }
        public int FactorId { get; set; }
        public int SubFactorId { get; set; }
        public int CountryId { get; set; }
        public decimal? Volume { get; set; }
        //Used for calculation
        public RiskRating RiskRating { get; set; }
    }
}
