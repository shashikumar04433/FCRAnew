using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Mappings;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Responses;
using Microsoft.AspNetCore.Http;

namespace FCRA.ViewModels
{
    public class AssessmentPillsViewModel
    {
        public StageViewModel? Stage { get; set; }
        public RiskTypeViewModel? RiskType { get; set; }
        public GeographicPresenceViewModel? GeographicPresence { get; set; }
        public CustomerSegmentViewModel? CustomerSegment { get; set; }
        public BusinessSegmentViewModel? BusinessSegment { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }

        public List<RiskFactorViewModel> RiskFactors { get; set; } = new();
        public List<RiskSubFactorViewModel> RiskSubFactors { get; set; } = new();
        public List<RiskFactorProductServiceMappingViewModel> ProductServiceMappings { get; set; } = new();
        public List<RiskScoreResponseViewModel> RiskCriteriaMappings { get; set; } = new();
        public List<RiskSubFactorVolumeResponseViewModel> VolumeMappings { get; set; } = new();

        public static AssessmentPillsViewModel GetAssesmentModel(AssessmentPillsViewModel thisModel, int riskFactorId)
        {
            AssessmentPillsViewModel model = new()
            {
                Stage = thisModel.Stage,
                RiskType = thisModel.RiskType,
                GeographicPresence = thisModel.GeographicPresence,
                CustomerSegment = thisModel.CustomerSegment,
                BusinessSegment = thisModel.BusinessSegment,
                RiskFactors = thisModel.RiskFactors.Where(t => t.Id == riskFactorId).ToList()
            };
            if (model.RiskFactors.Any())
            {
                model.ProductServiceMappings = thisModel.ProductServiceMappings.Where(t => t.RiskFactorId == riskFactorId).ToList();
                model.RiskCriteriaMappings = thisModel.RiskCriteriaMappings.Where(t => t.RiskFactorId == riskFactorId).ToList();
                model.VolumeMappings = thisModel.VolumeMappings.Where(t => t.RiskFactorId == riskFactorId).ToList();
            }
            return model;
        }
        public static AssessmentPillsViewModel GetAssesmentModel(AssessmentPillsViewModel thisModel, int riskFactorId, int riskSubFactorId)
        {
            AssessmentPillsViewModel model = new()
            {
                Stage = thisModel.Stage,
                RiskType = thisModel.RiskType,
                GeographicPresence = thisModel.GeographicPresence,
                CustomerSegment = thisModel.CustomerSegment,
                BusinessSegment = thisModel.BusinessSegment,
                RiskFactors = thisModel.RiskFactors.Where(t => t.Id == riskFactorId).ToList()
            };
            if (model.RiskFactors.Any())
            {
                model.ProductServiceMappings = thisModel.ProductServiceMappings.Where(t => t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId).ToList();
                model.RiskCriteriaMappings = thisModel.RiskCriteriaMappings.Where(t => t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId).ToList();
                model.VolumeMappings = thisModel.VolumeMappings.Where(t => t.RiskFactorId == riskFactorId && t.RiskSubFactorId == riskSubFactorId).ToList();
            }
            return model;
        }
        public List<RiskSubFactorAttachmentViewModel> RiskSubFactorAttachment { get; set; } = new();
        public List<ApprovalRequestViewModel> ApprovalRequests { get; set; } = new();
        public List<UserViewModel> UserViewModels { get; set; } = new();
    }
}
