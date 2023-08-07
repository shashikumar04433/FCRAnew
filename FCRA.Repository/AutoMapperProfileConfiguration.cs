using AutoMapper;
using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Base;
using FCRA.Models.Customers;
using FCRA.Models.Defaults;
using FCRA.Models.Mappings;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.ViewModels;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Base;
using FCRA.ViewModels.Customers;
using FCRA.ViewModels.Defaults;
using FCRA.ViewModels.Mappings;
using FCRA.ViewModels.Masters;
using FCRA.ViewModels.Responses;

namespace FCRA.Repository
{
    internal class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<BaseModel, BaseViewModel>().ReverseMap();
            CreateMap<BaseMasterModel, BaseMasterViewModel>().ReverseMap();
            #region Master
            CreateMap<UserMaster, UserViewModel>().ReverseMap();
            CreateMap<UserRoles, UserRolesViewModel>().ReverseMap();
            CreateMap<UserType, UserTypeViewModel>().ReverseMap();
            CreateMap<RoleMaster, RoleMasterViewModel>().ReverseMap();
            CreateMap<FormMaster, FormViewModel>().ReverseMap();
            CreateMap<DefaultScale, DefaultScaleViewModel>().ReverseMap();
            CreateMap<Country, CountryViewModel>().ReverseMap();
            CreateMap<GeographyRisk, GeographyRiskViewModel>().ReverseMap();
            CreateMap<CustomerForm, CustomerFormViewModel>().ReverseMap();
            CreateMap<CustomerLocation, CustomerLocationViewModel>().ReverseMap();
            CreateMap<CustomerScaleLabel, CustomerScaleLabelViewModel>().ReverseMap();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<Stage, StageViewModel>().ReverseMap();
            CreateMap<RiskType, RiskTypeViewModel>().ReverseMap();
            CreateMap<GeographicPresence, GeographicPresenceViewModel>().ReverseMap();
            CreateMap<CustomerSegment, CustomerSegmentViewModel>().ReverseMap();
            CreateMap<BusinessSegment, BusinessSegmentViewModel>().ReverseMap();

            CreateMap<RiskFactor, RiskFactorViewModel>().ReverseMap();
            CreateMap<RiskSubFactor, RiskSubFactorViewModel>().ReverseMap();

            CreateMap<PreDefinedRiskParameter, PreDefinedRiskParameterViewModel>().ReverseMap();
            CreateMap<ProductService, ProductServiceViewModel>().ReverseMap();
            CreateMap<Questions, QuestionsViewModel>().ReverseMap();
            CreateMap<RiskCriteria, RiskCriteriaViewModel>().ReverseMap();

            CreateMap<RiskFactorProductServiceMapping, RiskFactorProductServiceMappingViewModel>().ReverseMap();
            CreateMap<ProductRiskCriteriaMapping, ProductRiskCriteriaMappingViewModel>().ReverseMap();
            CreateMap<QuestionsRiskCriteriaMapping, QuestionsRiskCriteriaMappingViewModel>().ReverseMap();

            CreateMap<ApprovalRequest, ApprovalRequestViewModel>().ReverseMap();
            CreateMap<ApprovalHistory, ApprovalHistoryViewModel>().ReverseMap();

            CreateMap<DataAuditTrail, DataAuditTrailViewModel>().ReverseMap();
            CreateMap<CustomerConfiguration, CustomerConfigurationViewModel>().ReverseMap();
            CreateMap<CustomerVersionMaster, CustomerVersionMasterViewModel>().ReverseMap();

            #endregion

            CreateMap<RiskFactorResponse, RiskFactorResponseViewModel>().ReverseMap();
            CreateMap<RiskScoreResponse, RiskScoreResponseViewModel>().ReverseMap();
            CreateMap<RiskSubFactorResponse, RiskSubFactorResponseViewModel>().ReverseMap();
            CreateMap<RiskScoreProductVolumRatingResponse, RiskScoreProductVolumRatingResponseViewModel>().ReverseMap();
            CreateMap<RiskSubFactorVolumeResponse, RiskSubFactorVolumeResponseViewModel>().ReverseMap();

            CreateMap<ExitRemarks, ExitRemarksViewModel>().ReverseMap();
            CreateMap<RiskSubFactorAttachment, RiskSubFactorAttachmentViewModel>().ReverseMap();
            CreateMap<ApprovalRequest, ApprovalRequestViewModel>().ReverseMap();
            CreateMap<ApprovalHistory, ApprovalHistoryViewModel>().ReverseMap();
            CreateMap<ApprovalMatrix, ApprovalMatrixViewModel>().ReverseMap();

            #region Approval

            CreateMap<ApprovedRiskFactorResponse, RiskFactorResponseViewModel>().ReverseMap();
            CreateMap<ApprovedRiskScoreResponse, RiskScoreResponseViewModel>().ReverseMap();
            CreateMap<ApprovedRiskSubFactorResponse, RiskSubFactorResponseViewModel>().ReverseMap();
            CreateMap<ApprovedRiskScoreProductVolumRatingResponse, RiskScoreProductVolumRatingResponseViewModel>().ReverseMap();
            CreateMap<ApprovedRiskSubFactorVolumeResponse, RiskSubFactorVolumeResponseViewModel>().ReverseMap();
            CreateMap<ApprovedRiskSubFactorAttachment, RiskSubFactorAttachmentViewModel>().ReverseMap();

            #endregion
        }
    }
}
