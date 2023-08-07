using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Common
{
    public enum FormDefination : int
    {
        [Display(Name = "Role")]
        Role = 1,
        [Display(Name = "Role Permission")]
        RolePermissions = 2,
        [Display(Name = "User")]
        User = 3,
        [Display(Name = "Country")]
        Country = 4,
        [Display(Name = "Customer")]
        Customer = 5,
        [Display(Name = "Geography Risk - Country Risk Rating")]
        GeographyRisk = 6,
        [Display(Name = "Stage")]
        Stage = 7,
        [Display(Name = "Risk Type")]
        RiskType = 8,
        [Display(Name = "Geographic Presence")]
        GeographicPresence = 9,
        [Display(Name = "Business Segment")]
        CustomerSegment = 10,
        [Display(Name = "Sub Unit")]
        BusinessSegment = 11,
        [Display(Name = "Risk Factor")]
        RiskFactor = 12,
        [Display(Name = "Risk Sub Factor")]
        RiskSubFactor = 13,
        [Display(Name = "Pre DefinedRisk Parameter")]
        PreDefinedRiskParameter = 14,
        [Display(Name = "Product Service")]
        ProductService = 15,
        [Display(Name = "Question")]
        Questions = 16,
        [Display(Name = "Risk Criteria")]
        RiskCriteria = 17,
        [Display(Name = "Risk Factor Product Service Mapping")]
        RiskFactorProductServiceMapping = 18,
        [Display(Name = "Product Risk Criteria Mapping")]
        ProductRiskCriteriaMapping = 19,
        [Display(Name = "Risk Assessment")]
        RiskAssessment = 20,
        [Display(Name = "Summary")]
        Home = 21,
        [Display(Name = "Audit Log")]
        ExitRemarks = 22,
        [Display(Name = "Customer Configuration")]
        CustomerConfiguration = 23,
        [Display(Name = "Approval Matrix")]
        ApprovalMatrix = 24,
        [Display(Name = "Risk Assessment Version")]
        RiskAssessmentVersion = 25,
        [Display(Name = "Approval Status")]
        ApprovalStatus = 26,
    }
}
