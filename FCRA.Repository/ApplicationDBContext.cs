using FCRA.Common;
using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Customers;
using FCRA.Models.Mappings;
using FCRA.Models.Masters;
using FCRA.Models.Responses;
using FCRA.Repository.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FCRA.Repository
{
    internal class ApplicationDBContext : DbContext
    {
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<RoleMaster> RoleMaster { get; set; }
        public DbSet<MenuMaster> MenuMaster { get; set; }
        public DbSet<FormMaster> FormMaster { get; set; }
        public DbSet<FormControlMaster> FormControlMaster { get; set; }
        public DbSet<FormControlRoleMaster> FormControlRoleMaster { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }

        //Customer Start
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerLocation> CustomerLocations { get; set; }
        public DbSet<CustomerScaleLabel> CustomerScaleLabels { get; set; }
        public DbSet<GeographyRisk> CustomerCountries { get; set; }
        public DbSet<CustomerForm> CustomerForms { get; set; }
        public DbSet<CustomerConfiguration> CustomerConfigurations { get; set; }
        //Customer End


        //Masters start
        public DbSet<Stage> Stages { get; set; }
        public DbSet<RiskType> RiskTypes { get; set; }
        public DbSet<GeographicPresence> GeographicPresences { get; set; }
        public DbSet<CustomerSegment> CustomerSegments { get; set; }
        public DbSet<BusinessSegment> BusinessSegments { get; set; }
        public DbSet<RiskFactor> RiskFactors { get; set; }
        public DbSet<RiskSubFactor> RiskSubFactors { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<PreDefinedRiskParameter> PreDefinedRiskParameters { get; set; }
        public DbSet<ProductService> ProductServices { get; set; }
        public DbSet<RiskCriteria> RiskCriterias { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public DbSet<ApprovalHistory> ApprovalHistorys { get; set; }
        public DbSet<DataAuditTrail> DataAuditTrails { get; set; }
        public DbSet<CustomerVersionMaster> CustomerVersionMasters { get; set; }
        public DbSet<ApprovalMatrix> ApprovalMatrixs { get; set; }

        //Masters end
        //Mapping start
        public DbSet<RiskFactorProductServiceMapping> RiskFactorProductServiceMappings { get; set; }
        public DbSet<ProductRiskCriteriaMapping> ProductRiskCriteriaMappings { get; set; }
        public DbSet<QuestionsRiskCriteriaMapping> QuestionsRiskCriteriaMappings { get; set; }
        //Mapping end

        //Response start
        public DbSet<RiskFactorResponse> RiskFactorResponses { get; set; }
        public DbSet<RiskScoreResponse> RiskScoreResponses { get; set; }
        public DbSet<RiskSubFactorResponse> RiskSubFactorResponses { get; set; }
        public DbSet<RiskSubFactorVolumeResponse> RiskSubFactorVolumeResponses { get; set; }
        public DbSet<RiskScoreProductVolumRatingResponse> RiskScoreProductVolumRatingResponses { get; set; }
        public DbSet<RiskSubFactorAttachment> RiskSubFactorAttachments { get; set; }

        public DbSet<ApprovedRiskFactorResponse> ApprovedRiskFactorResponses { get; set; }
        public DbSet<ApprovedRiskScoreResponse> ApprovedRiskScoreResponses { get; set; }
        public DbSet<ApprovedRiskSubFactorResponse> ApprovedRiskSubFactorResponses { get; set; }
        public DbSet<ApprovedRiskSubFactorVolumeResponse> ApprovedRiskSubFactorVolumeResponses { get; set; }
        public DbSet<ApprovedRiskScoreProductVolumRatingResponse> ApprovedRiskScoreProductVolumRatingResponses { get; set; }
        public DbSet<ApprovedRiskSubFactorAttachment> ApprovedRiskSubFactorAttachments { get; set; }
        //Response end
        public DbSet<ExitRemarks> ExitRemarks { get; set; }






#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relations in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relations.DeleteBehavior = DeleteBehavior.NoAction;
            }

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                int iIndex = 3;
                var properties = entity.ClrType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).OrderBy(t => t.MetadataToken);
                foreach (var property in properties)
                {
                    var prop = entity.FindProperty(property.Name);
                    if (prop != null)
                    {
                        var ano = prop.FindAnnotation("Relational:ColumnOrder");
                        if (ano == null)
                        {
                            prop.AddAnnotation("Relational:ColumnOrder", iIndex);
                            iIndex += 1;
                        }
                    }
                }
            }


            //Seed Data start
            //Comment once migration completed
            builder.SeedCommonMasterData();
            builder.SeedMenuMasterData();
            builder.SeedFormMasterData();
            builder.SeedFormControlMasterData();



            builder.Entity<UserMaster>().HasOne(t => t.Role).WithMany().HasForeignKey(k => k.RoleId);
            builder.Entity<UserMaster>().HasOne(t => t.Customer).WithMany().HasForeignKey(k => k.CustomerId);
            builder.Entity<UserRoles>().HasKey(t => new { t.UserId, t.RoleId });
            builder.Entity<FormControlMaster>().HasKey(t => new { t.FormId, t.ControlId });
            builder.Entity<RolePermissions>().HasKey(t => new { t.RoleId, t.FormId });


            builder.Entity<CustomerLocation>().HasKey(t => new { t.CustomerId, t.LocationId });
            builder.Entity<CustomerScaleLabel>().HasKey(t => new { t.CustomerId, t.ScaleId });
            builder.Entity<CustomerForm>().HasKey(t => new { t.CustomerId, t.FormId });
            builder.Entity<GeographyRisk>().Property(t => t.RiskRating).HasDefaultValue(RiskRating.Low);

            builder.Entity<RiskFactorProductServiceMapping>().HasKey(t => new { t.CustomerId, t.RiskFactorId, t.RiskSubFactorId, t.ProductId });
            builder.Entity<ProductRiskCriteriaMapping>().HasKey(t => new { t.CustomerId, t.RiskFactorId, t.RiskSubFactorId, t.ProductId, t.RiskCriteriaId });
            builder.Entity<QuestionsRiskCriteriaMapping>().HasKey(t => new { t.CustomerId, t.RiskFactorId, t.RiskSubFactorId, t.ProductId, t.RiskCriteriaId, t.QuestionId });

            builder.Entity<RiskScoreResponse>().HasKey(t => new { t.CustomerId, t.RiskFactorId, t.RiskSubFactorId, t.ProductId, t.RiskCriteriaId });
            builder.Entity<RiskSubFactorResponse>().HasKey(t => new { t.CustomerId, t.RiskFactorId, t.RiskSubFactorId });
            builder.Entity<RiskSubFactorVolumeResponse>().HasKey(t => new { t.CustomerId, t.RiskFactorId, t.RiskSubFactorId });
            builder.Entity<RiskScoreProductVolumRatingResponse>().HasKey(t => new { t.CustomerId, t.RiskFactorId, t.RiskSubFactorId, t.ProductId });
                  
            builder.Entity<RiskScoreProductVolumRatingResponse>().Property(t => t.RiskRating).HasDefaultValue(1);





            base.OnModelCreating(builder);
        }
    }
}
