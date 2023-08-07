using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FCRA.ViewModels.Mappings;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Responses;

namespace FCRA.ViewModels
{
    public class AssessmentViewModel
    {
        public RiskTypeViewModel RiskType { get; set; } = new();
        public List<RiskFactorViewModel> RiskFactors { get; set; } = new();
        public List<RiskSubFactorViewModel> RiskSubFactors { get; set; } = new();
        public List<RiskFactorProductServiceMappingViewModel> ProductServiceMappings { get; set; } = new();
        public List<RiskScoreResponseViewModel> RiskCriteriaMappings { get; set; } = new();
        public List<RiskSubFactorVolumeResponseViewModel> VolumeMappings { get; set; } = new();

        public static AssessmentViewModel GetAssesmentModel(AssessmentViewModel thisModel, int riskFactorId)
        {
            AssessmentViewModel model = new();
            model.RiskType = thisModel.RiskType;
            model.RiskFactors = thisModel.RiskFactors.Where(t => t.Id == riskFactorId).ToList();
            if (model.RiskFactors.Any())
            {
                model.ProductServiceMappings = thisModel.ProductServiceMappings.Where(t => t.RiskFactorId == riskFactorId).ToList();
                model.RiskCriteriaMappings = thisModel.RiskCriteriaMappings.Where(t => t.RiskFactorId == riskFactorId).ToList();
                model.VolumeMappings = thisModel.VolumeMappings.Where(t => t.RiskFactorId == riskFactorId).ToList();
            }
            return model;
        }
        public static AssessmentViewModel GetAssesmentModel(AssessmentViewModel thisModel, int riskFactorId, int riskSubFactorId)
        {
            AssessmentViewModel model = new();
            model.RiskType = thisModel.RiskType;
            model.RiskFactors = thisModel.RiskFactors.Where(t => t.Id == riskFactorId).ToList();
            if (model.RiskFactors.Any())
            {
                model.ProductServiceMappings = thisModel.ProductServiceMappings.Where(t => t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId).ToList();
                model.RiskCriteriaMappings = thisModel.RiskCriteriaMappings.Where(t => t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId).ToList();
                model.VolumeMappings = thisModel.VolumeMappings.Where(t => t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId).ToList();
            }
            return model;
        }
    }
}
