using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Responses.Excel
{
    public class ExcelResponseViewModel
    {
        public List<ExcelRiskSubFactorResponse> SubFactorResponses { get; set; } = new();
        public List<ExcelRiskVolumeResponse> VolumeResponses { get; set; } = new();
        public List<ExcelRiskProductResponse> ProductResponses { get; set; } = new();
    }
}
