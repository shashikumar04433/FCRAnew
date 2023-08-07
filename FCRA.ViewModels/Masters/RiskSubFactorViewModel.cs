using FCRA.Common;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace FCRA.ViewModels.Masters
{
    public class RiskSubFactorViewModel : BaseMasterCustomerViewModel
    {
        [MapToDTO, Required, MaxLength(int.MaxValue)]
        public override string? Name { get; set; }
        [MapToDTO, Required, MaxLength(int.MaxValue)]
        public override string? Description { get; set; }
        [MapToDTO]
        [Required]
        [Display(Name = "Risk Factor")]
        public int RiskFactorId { get; set; }
        //Percent start
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 1", ErrorMessage = "{0} is required")]
        [DecimalNumber(2, 2)]
        [Display(Name = "Percentage 2")]
        public decimal? Percentage2 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 1", ErrorMessage = "{0} is required")]
        [DecimalNumber(2, 2)]
        [Display(Name = "Percentage 3")]
        public decimal? Percentage3 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 1 && ({nameof(ScaleType)} == 4 || {nameof(ScaleType)} == 5)", ErrorMessage = "{0} is required")]
        [DecimalNumber(2, 2)]
        [Display(Name = "Percentage 4")]
        public decimal? Percentage4 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 1 && {nameof(ScaleType)} == 5", ErrorMessage = "{0} is required")]
        [DecimalNumber(2, 2)]
        [Display(Name = "Percentage 5")]
        public decimal? Percentage5 { get; set; }
        //Percent end
        //Pre-defined start
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 2", ErrorMessage = "{0} is required")]
        [Display(Name = "Parameter 1")]
        public int? PreDefinedParameter1Id { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 2", ErrorMessage = "{0} is required")]
        [Display(Name = "Parameter 2")]
        public int? PreDefinedParameter2Id { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 2", ErrorMessage = "{0} is required")]
        [Display(Name = "Parameter 3")]
        public int? PreDefinedParameter3Id { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 2 && ({nameof(ScaleType)} == 4 || {nameof(ScaleType)} == 5)", ErrorMessage = "{0} is required")]
        [Display(Name = "Parameter 4")]
        public int? PreDefinedParameter4Id { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 2 && {nameof(ScaleType)} == 5", ErrorMessage = "{0} is required")]
        [Display(Name = "Parameter 5")]
        public int? PreDefinedParameter5Id { get; set; }
        //Pre-defined end
        //Descriptive start
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 3", ErrorMessage = "{0} is required")]
        [Display(Name = "Description 1")]
        public string? RiskDescription1 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 3", ErrorMessage = "{0} is required")]
        [Display(Name = "Description 2")]
        public string? RiskDescription2 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 3", ErrorMessage = "{0} is required")]

        [Display(Name = "Description 3")]
        public string? RiskDescription3 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 3 && ({nameof(ScaleType)} == 4 || {nameof(ScaleType)} == 5)", ErrorMessage = "{0} is required")]
        [Display(Name = "Description 4")]
        public string? RiskDescription4 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 3 && {nameof(ScaleType)} == 5", ErrorMessage = "{0} is required")]
        [Display(Name = "Description 5")]
        public string? RiskDescription5 { get; set; }
        //Descriptive end
        //Volume Start
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 4", ErrorMessage = "{0} is required")]
        [DecimalNumber]
        [Display(Name = "Volume 1")]
        public decimal? RiskVolume1 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 4", ErrorMessage = "{0} is required")]
        [DecimalNumber]
        [Display(Name = "Volume 2")]
        public decimal? RiskVolume2 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 4", ErrorMessage = "{0} is required")]
        [DecimalNumber]
        [Display(Name = "Volume 3")]
        public decimal? RiskVolume3 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 4 && ({nameof(ScaleType)} == 4 || {nameof(ScaleType)} == 5)", ErrorMessage = "{0} is required")]
        [DecimalNumber]
        [Display(Name = "Volume 4")]
        public decimal? RiskVolume4 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 4 && {nameof(ScaleType)} == 5", ErrorMessage = "{0} is required")]
        [DecimalNumber]
        [Display(Name = "Volume 5")]
        public decimal? RiskVolume5 { get; set; }
        //Volume End
        //Number start
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 6", ErrorMessage = "{0} is required")]
        [DecimalNumber]
        [Display(Name = "Number 2")]
        public decimal? Number2 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 6", ErrorMessage = "{0} is required")]
        [DecimalNumber]
        [Display(Name = "Number 3")]
        public decimal? Number3 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 6 && ({nameof(ScaleType)} == 4 || {nameof(ScaleType)} == 5)", ErrorMessage = "{0} is required")]
        [DecimalNumber]
        [Display(Name = "Number 4")]
        public decimal? Number4 { get; set; }
        [MapToDTO]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false && {nameof(RiskRangeParameter)} == 6 && {nameof(ScaleType)} == 5", ErrorMessage = "{0} is required")]
        [DecimalNumber]
        [Display(Name = "Number 5")]
        public decimal? Number5 { get; set; }
        //Number end
        [MapToDTO]
        [Display(Name = "Risk Weightage %")]
        [RequiredIf($"{nameof(IsExcludedInRisk)} == false ", ErrorMessage = "{0} is required")]
        [DecimalNumber(3, 2)]
        public decimal? RiskWeightage { get; set; }

        [MapToDTO, Display(Name = "Display Order")]
        public int Sequence { get; set; }
        [MapToDTO, Display(Name = "Is Attachment Applicable")]
        public bool isAttachmentApplicable { get; set; }
        [MapToDTO, Display(Name = "Is Attachment Mandatory")]
        public bool isAttachmentMandatory { get; set; }
        public virtual RiskFactorViewModel? RiskFactor { get; set; }
        public PreDefinedRiskParameterViewModel? PreDefinedRiskParameter1 { get; set; }
        public PreDefinedRiskParameterViewModel? PreDefinedRiskParameter2 { get; set; }
        public PreDefinedRiskParameterViewModel? PreDefinedRiskParameter3 { get; set; }
        public PreDefinedRiskParameterViewModel? PreDefinedRiskParameter4 { get; set; }
        public PreDefinedRiskParameterViewModel? PreDefinedRiskParameter5 { get; set; }

        //Not mapped fields
        [NotMapped]
        public ScaleType ScaleType { get; set; } = ScaleType.ThreePoint;
        [NotMapped]
        public bool IsExcludedInRisk { get; set; }
        [NotMapped]
        public RiskRangeParameter RiskRangeParameter { get; set; } = RiskRangeParameter.PercentRange;
        [NotMapped, Required, Display(Name = "Stage")]
        public int? StageId { get; set; }
        [NotMapped, Display(Name = "Risk Type")]
        public int? RiskTypeId { get; set; }
        [NotMapped, Display(Name = "Geographic Presence")]
        public int? GeographicPresenceId { get; set; }
        [NotMapped, MapToDTO, Display(Name = "Business Segment")]
        public int? CustomerSegmentId { get; set; }
        [NotMapped, MapToDTO, Display(Name = "Sub Unit")]
        public int? BusinessSegmentId { get; set; }
        [NotMapped, Display(Name = "Pre Defined Parameter")]
        public int? PreDefinedParameterId { get; set; }
        [NotMapped]
        public string? ResponseDescription { get; set; }

        [NotMapped]
        [DecimalNumber]
        public decimal? Response { get; set; }
        [NotMapped]
        public string? Assumptions { get; set; }
        [NotMapped]
        public int? Score { get; set; }
        [NotMapped, Display(Name = "File")]
        public IFormFile? FormFile { get; set; }

        public List<RiskSubFactorAttachmentViewModel> riskSubFactorAttachment { get; set; } = new();
     
    }
}
