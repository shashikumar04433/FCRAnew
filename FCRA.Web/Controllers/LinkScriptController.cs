using FCRA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace FCRA.Web.Controllers
{
    public class LinkScriptController : Controller
    {
        //private readonly StorageSettings _storageSettings;
        //public LinkScriptController(IOptions<StorageSettings> options)
        public LinkScriptController()
        {
            //_storageSettings = options.Value;
        }
        public JavaScriptResult PermissionScript(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return new JavaScriptResult(string.Empty);
            return new JavaScriptResult(q.Decrypt());
        }
        
        public JavaScriptResult AdminRolePermissions()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getRolePermissionsUrl='{Url.Action("GetRolePermissions", "RolePermissions", new { area = "Admin" })}';");
            stringBuilder.Append($"var updateRolePermissionsUrl='{Url.Action("UpdateRolePermissions", "RolePermissions", new { area = "Admin" })}';");
            stringBuilder.Append($"var GetApprovalMatrixUrl='{Url.Action("GetApprovalMatrix", "ApprovalMatrix", new { area = "" })}';");
            stringBuilder.Append($"var saveApprovalMatrixUrl='{Url.Action("SaveApprovalMatrix", "ApprovalMatrix", new { area = "" })}';");
            stringBuilder.Append($"var GetApprovalStatusUrl='{Url.Action("GetApprovalStatus", "ApprovalStatus", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult AdminProductServiceMapping() {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getProductMappingUrl='{Url.Action("GetProductMapping", "ProductServiceMapping", new { area = "Admin" })}';");
            stringBuilder.Append($"var updateProductMappingUrl='{Url.Action("UpdateProductMapping", "ProductServiceMapping", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult AdminProductRiskCriteriaMapping()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getCriteriaMappingUrl='{Url.Action("GetCriteriaMapping", "ProductRiskCriteriaMapping", new { area = "Admin" })}';");
            stringBuilder.Append($"var updateCriteriaMappingUrl='{Url.Action("UpdateCriteriaMapping", "ProductRiskCriteriaMapping", new { area = "Admin" })}';");
            stringBuilder.Append($"var getProductQuestionsUrl='{Url.Action("GetProductQuestions", "ProductRiskCriteriaMapping", new { area = "Admin" })}';");
            stringBuilder.Append($"var getProductQuestionsAddedUrl='{Url.Action("GetProductQuestionsAdded", "ProductRiskCriteriaMapping", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult Global()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getUserMenuUrl='{Url.Action("LoadMenu", "Account", new { area = "" })}';");
            stringBuilder.Append($"var userNotificationGetUrl='{Url.Action("UserNotificationGet", "Account", new { area = "" })}';");
            stringBuilder.Append($"var addExitRemarksUrl='{Url.Action("Index", "ExitRemarks", new { area = "" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
       
        public JavaScriptResult HomeIndex()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var dashboardUserUrl='{Url.Action("DashboardUser", "Home", new { area = "" })}';");
            stringBuilder.Append($"var summaryDetailsUrl='{Url.Action("SummaryDetails", "Summary", new { area = "" })}';");
            stringBuilder.Append($"var dashboardDetailsUrl='{Url.Action("DashboardDetails", "Summary", new { area = "" })}';");
            stringBuilder.Append($"var comparisonSummaryUrl='{Url.Action("Comparison", "Summary", new { area = "" })}';");
            stringBuilder.Append($"var GetRiskFactorComparisonUrl='{Url.Action("GetRiskFactorComparison", "Summary", new { area = "" })}';");
            stringBuilder.Append($"var SummaryReportUrl='{Url.Action("SummaryReports", "Home", new { area = "" })}';");
            stringBuilder.Append($"var SummaryVersionReportUrl='{Url.Action("SummaryVersionReports", "RiskAssessmentVersion", new { area = "Admin" })}';");
            stringBuilder.Append($"var DownloadUrl='{Url.Action("Download", "RiskAssessmentVersion", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }

        public JavaScriptResult RiskAssessmentAssessment()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var saveResponseUrl='{Url.Action("SaveResponse", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var getQuestionRiskCriteriaMappingUrl='{Url.Action("GetQuestionRiskCriteriaMapping", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var getCountriesUrl='{Url.Action("GetCountries", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var SubFactorTempFileItemAddUrl='{Url.Action("SubFactorTempFileAdd", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var SubmitApprovalRequestUrl='{Url.Action("SubmitApprovalRequest", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var saveApprovedResponseUrl='{Url.Action("SaveApprovedResponse", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var SendEmailUrl='{Url.Action("SendEmail", "RiskAssessment", new { area = "" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }

        public JavaScriptResult AdminCustomer()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getLocationTemplateUrl='{Url.Action("GetLocationTemplate", "Customer", new { area = "Admin" })}';");
            stringBuilder.Append($"var getCountryTemplateUrl='{Url.Action("GetCountryTemplate", "Customer",  new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult AdminCommonCategory()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getRiskTypeOptionsUrl='{Url.Action("GetRiskTypeOptions", "CommonCategory", new { area = "Admin" })}';");
            stringBuilder.Append($"var getGeographicPresenceOptionsUrl='{Url.Action("GetGeographicPresenceOptions", "CommonCategory", new { area = "Admin" })}';");
            stringBuilder.Append($"var getCustomerSegmentOptionsUrl='{Url.Action("GetCustomerSegmentOptions", "CommonCategory", new { area = "Admin" })}';");
            stringBuilder.Append($"var getBusinessSegmentOptionsUrl='{Url.Action("GetBusinessSegmentOptions", "CommonCategory", new { area = "Admin" })}';");
            stringBuilder.Append($"var getRiskFactorOptionsUrl='{Url.Action("GetRiskFactorOptions", "CommonCategory", new { area = "Admin" })}';");
            stringBuilder.Append($"var getRiskSubFactorOptionsUrl='{Url.Action("GetRiskSubFactorOptions", "CommonCategory", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult AdminGeographicPresence()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getRiskTypeOptionsUrl='{Url.Action("GetRiskTypeOptions", "GeographicPresence", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult AdminCustomerSegment()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getRiskTypeOptionsUrl='{Url.Action("GetRiskTypeOptions", "CustomerSegment", new { area = "Admin" })}';");
            stringBuilder.Append($"var getGeographicPresenceOptionsUrl='{Url.Action("GetGeographicPresenceOptions", "CustomerSegment", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }

        public JavaScriptResult AdminBusinessSegment()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getRiskTypeOptionsUrl='{Url.Action("GetRiskTypeOptions", "BusinessSegment", new { area = "Admin" })}';");
            stringBuilder.Append($"var getGeographicPresenceOptionsUrl='{Url.Action("GetGeographicPresenceOptions", "BusinessSegment", new { area = "Admin" })}';");
            stringBuilder.Append($"var getCustomerSegmentOptionsUrl='{Url.Action("GetCustomerSegmentOptions", "BusinessSegment", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }

        public JavaScriptResult AdminRiskFactor()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getRiskTypeOptionsUrl='{Url.Action("GetRiskTypeOptions", "RiskFactor", new { area = "Admin" })}';");
            stringBuilder.Append($"var getGeographicPresenceOptionsUrl='{Url.Action("GetGeographicPresenceOptions", "RiskFactor", new { area = "Admin" })}';");
            stringBuilder.Append($"var getCustomerSegmentOptionsUrl='{Url.Action("GetCustomerSegmentOptions", "RiskFactor", new { area = "Admin" })}';");
            stringBuilder.Append($"var getBusinessSegmentOptionsUrl='{Url.Action("GetBusinessSegmentOptions", "RiskFactor", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult AdminRiskSubFactor()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getRiskTypeOptionsUrl='{Url.Action("GetRiskTypeOptions", "RiskSubFactor", new { area = "Admin" })}';");
            stringBuilder.Append($"var getGeographicPresenceOptionsUrl='{Url.Action("GetGeographicPresenceOptions", "RiskSubFactor", new { area = "Admin" })}';");
            stringBuilder.Append($"var getCustomerSegmentOptionsUrl='{Url.Action("GetCustomerSegmentOptions", "RiskSubFactor", new { area = "Admin" })}';");
            stringBuilder.Append($"var getBusinessSegmentOptionsUrl='{Url.Action("GetBusinessSegmentOptions", "RiskSubFactor", new { area = "Admin" })}';");
            stringBuilder.Append($"var getRiskFactorOptionsUrl='{Url.Action("GetRiskFactorOptions", "RiskSubFactor", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult RiskAssessmentAssessmentIndexPills()
        {
            StringBuilder stringBuilder = new();            
            stringBuilder.Append($"var getRiskTypeListUrl='{Url.Action("GetRiskTypeCategoryList", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var getGeographicsPresenceListUrl='{Url.Action("GetGeographicsPresenceCategoryList", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var getCustomerSegmentListUrl='{Url.Action("GetCustomerSegmentCategoryList", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var getBusinessSegmentListUrl='{Url.Action("GetBusinessSegmentCategoryList", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var getAssessmentPillsUrl='{Url.Action("AssessmentPills", "RiskAssessment", new { area = "" })}';");
            stringBuilder.Append($"var uploadAssesmentExcelUrl='{Url.Action("UploadAssesment", "RiskAssessment", new { area = "" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult CategoryPills()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var bHasCategoryProgress=false;");
            stringBuilder.Append($"var getRiskTypeListUrl='{Url.Action("GetRiskTypeCategoryList", "CategoryPills", new { area = "" })}';");
            stringBuilder.Append($"var getGeographicsPresenceListUrl='{Url.Action("GetGeographicsPresenceCategoryList", "CategoryPills", new { area = "" })}';");
            stringBuilder.Append($"var getCustomerSegmentListUrl='{Url.Action("GetCustomerSegmentCategoryList", "CategoryPills", new { area = "" })}';");
            stringBuilder.Append($"var getBusinessSegmentListUrl='{Url.Action("GetBusinessSegmentCategoryList", "CategoryPills", new { area = "" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult AdminMastersCustomer()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getAuditTrailUrl='{Url.Action("GetAuditTrail", "AuditTrail", new { area = "Admin" })}';");
            stringBuilder.Append($"var getRiskAssessmentAuditTrailurl='{Url.Action("GetRiskAssessmentAuditTrail", "AuditTrail", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
        public JavaScriptResult AdminCustomerConfiguration()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"var getCustomerConfigurationUrl='{Url.Action("GetCustomerConfiguration", "CustomerConfiguration", new { area = "Admin" })}';");
            stringBuilder.Append($"var updateCustomerConfigurationUrl='{Url.Action("UpdateCustomerConfiguration", "CustomerConfiguration", new { area = "Admin" })}';");
            return new JavaScriptResult(stringBuilder.ToString());
        }
    }
}
