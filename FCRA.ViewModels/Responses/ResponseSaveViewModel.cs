using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Responses
{
    public class ResponseSaveViewModel
    {
        public List<RiskFactorResponseViewModel> RiskFactorResponses { get; set; } = new();
        public List<RiskSubFactorResponseViewModel> RiskSubFactorResponses { get; set; } = new();
        public List<RiskScoreProductVolumRatingResponseViewModel> RiskScoreProductVolumRatingResponses { get; set; } = new();
        public List<RiskScoreResponseViewModel> RiskScoreResponses { get; set; } = new();
        public List<RiskSubFactorVolumeResponseViewModel> VolumeMappings { get; set; } = new();
        public List<RiskSubFactorAttachmentViewModel> RiskSubFactorAttachments { get; set; } = new();
    }
}
