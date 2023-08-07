using FCRA.Common;
using FCRA.Models.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.SeedData
{
    internal static class FormMasterData
    {
        public static void SeedFormMasterData(this ModelBuilder builder)
        {
            //DateTime CreatedOn = new (Constants.CreatedOnYear, Constants.CreatedOnMonth, Constants.CreatedOnDay);
            builder.Entity<FormMaster>().HasData(
                //new FormMaster() { Id = (int)FormDefination.Country, Name = FormDefination.Country.GetDisplayName(), Area = "Admin", Controller = "Country", Action = "Index", IconClass = "fas fa-globe", Sequence = 1, MenuId = 1, IsAdmin = true }
                new FormMaster() { Id = (int)FormDefination.Customer, Name = FormDefination.Customer.GetDisplayName(), Area = "Admin", Controller = "Customer", Action = "Index", IconClass = "fas fa-user-friends", Sequence = 1, MenuId = 1, IsAdmin = true }
                //,new FormMaster() { Id = (int)FormDefination.CustomerConfiguration, Name = FormDefination.CustomerConfiguration.GetDisplayName(), Area = "Admin", Controller = "CustomerConfiguration", Action = "Index", IconClass = "fas fa-user-friends", Sequence = 2, MenuId = 1, IsAdmin = true }

                //Customer Details==4
                , new FormMaster() { Id = (int)FormDefination.Stage, Name = FormDefination.Stage.GetDisplayName(), Area = "Admin", Controller = "Stage", Action = "Index", IconClass = "fas fa-user-friends", Sequence = 8, MenuId = 4, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.RiskType, Name = FormDefination.RiskType.GetDisplayName(), Area = "Admin", Controller = "RiskType", Action = "Index", IconClass = "fas fa-asterisk", Sequence = 9, MenuId = 4, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.GeographicPresence, Name = FormDefination.GeographicPresence.GetDisplayName(), Area = "Admin", Controller = "GeographicPresence", Action = "Index", IconClass = "fas fa-user-friends", Sequence = 10, MenuId = 4, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.CustomerSegment, Name = FormDefination.CustomerSegment.GetDisplayName(), Area = "Admin", Controller = "CustomerSegment", Action = "Index", IconClass = "fas fa-user-friends", Sequence = 11, MenuId = 4, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.BusinessSegment, Name = FormDefination.BusinessSegment.GetDisplayName(), Area = "Admin", Controller = "BusinessSegment", Action = "Index", IconClass = "fas fa-briefcase", Sequence = 12, MenuId = 4, IsAdmin = true }


                //Mapping == 3
                , new FormMaster() { Id = (int)FormDefination.GeographyRisk, Name = FormDefination.GeographyRisk.GetDisplayName(), Area = "Admin", Controller = "GeographyRisk", Action = "Index", IconClass = "fas fa-user-friends", Sequence = 1, MenuId = 3, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.PreDefinedRiskParameter, Name = FormDefination.PreDefinedRiskParameter.GetDisplayName(), Area = "Admin", Controller = "PreDefinedRiskParameter", Action = "Index", IconClass = "fas fa-list-ul", Sequence = 2, MenuId = 3, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.ProductService, Name = FormDefination.ProductService.GetDisplayName(), Area = "Admin", Controller = "ProductService", Action = "Index", IconClass = "fas fa-credit-card", Sequence = 3, MenuId = 3, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.Questions, Name = FormDefination.Questions.GetDisplayName(), Area = "Admin", Controller = "Questions", Action = "Index", IconClass = "fas fa-question-circle", Sequence = 4, MenuId = 3, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.RiskCriteria, Name = FormDefination.RiskCriteria.GetDisplayName(), Area = "Admin", Controller = "RiskCriteria", Action = "Index", IconClass = "fas fa-paw", Sequence = 5, MenuId = 3, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.RiskFactorProductServiceMapping, Name = FormDefination.RiskFactorProductServiceMapping.GetDisplayName(), Area = "Admin", Controller = "ProductServiceMapping", Action = "Index", IconClass = "fas fa-sitemap", Sequence = 6, MenuId = 3, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.ProductRiskCriteriaMapping, Name = FormDefination.ProductRiskCriteriaMapping.GetDisplayName(), Area = "Admin", Controller = "ProductRiskCriteriaMapping", Action = "Index", IconClass = "fas fa-sitemap", Sequence = 7, MenuId = 3, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.RiskFactor, Name = FormDefination.RiskFactor.GetDisplayName(), Area = "Admin", Controller = "RiskFactor", Action = "Index", IconClass = "fas fa-industry", Sequence = 8, MenuId = 3, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.RiskSubFactor, Name = FormDefination.RiskSubFactor.GetDisplayName(), Area = "Admin", Controller = "RiskSubFactor", Action = "Index", IconClass = "fa fa-industry", Sequence = 9, MenuId = 3, IsAdmin = true }

                //User Master == 2
                , new FormMaster() { Id = (int)FormDefination.Role, Name = FormDefination.Role.GetDisplayName(), Area = "Admin", Controller = "Roles", Action = "Index", IconClass = "fas fa-building", Sequence = 1, MenuId = 2, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.RolePermissions, Name = FormDefination.RolePermissions.GetDisplayName(), Area = "Admin", Controller = "RolePermissions", Action = "Index", IconClass = "fas fa-building", Sequence = 2, MenuId = 2, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.User, Name = FormDefination.User.GetDisplayName(), Area = "Admin", Controller = "User", Action = "Index", IconClass = "fas fa-user-tie", Sequence = 3, MenuId = 2, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.ExitRemarks, Name = FormDefination.ExitRemarks.GetDisplayName(), Area = "", Controller = "ExitRemarks", Action = "Index", IconClass = "fas fa-comment-dots", Sequence = 4, MenuId = 2, IsAdmin = false }
                , new FormMaster() { Id = (int)FormDefination.ApprovalMatrix, Name = FormDefination.ApprovalMatrix.GetDisplayName(), Area = "", Controller = "ApprovalMatrix", Action = "Index", IconClass = "fas fa-building", Sequence = 5, MenuId = 2, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.ApprovalStatus, Name = FormDefination.ApprovalStatus.GetDisplayName(), Area = "Admin", Controller = "ApprovalStatus", Action = "Index", IconClass = "fas fa-building", Sequence = 6, MenuId = 2, IsAdmin = true }
                , new FormMaster() { Id = (int)FormDefination.RiskAssessmentVersion, Name = FormDefination.RiskAssessmentVersion.GetDisplayName(), Area = "Admin", Controller = "RiskAssessmentVersion", Action = "Index", IconClass = "fas fa-comment-dots", Sequence = 7, MenuId = 2, IsAdmin = true }


                //Others
                , new FormMaster() { Id = (int)FormDefination.RiskAssessment, Name = FormDefination.RiskAssessment.GetDisplayName(), Area = "", Controller = "RiskAssessment", Action = "Index", IconClass = "fas fa-tasks", Sequence = 1, MenuId = null, IsAdmin = false }
                , new FormMaster() { Id = (int)FormDefination.Home, Name = FormDefination.Home.GetDisplayName(), Area = "", Controller = "Home", Action = "Index", IconClass = "fas fa-chart-bar", Sequence = 2, MenuId = null, IsAdmin = false }
                //, new FormMaster() { Id = (int)FormDefination.RiskCompare, Name = FormDefination.RiskCompare.GetDisplayName(), Area = "", Controller = "RiskCompare", Action = "Index", IconClass = "fas fa-not-equal", Sequence = 2, MenuId = null, IsAdmin = false }
                );
        }
    }
}
