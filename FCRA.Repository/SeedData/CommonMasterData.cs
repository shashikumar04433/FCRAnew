using FCRA.Common;
using FCRA.Models.Account;
using FCRA.Models.Customers;
using FCRA.Models.Defaults;
using FCRA.Models.Masters;
using Microsoft.EntityFrameworkCore;

namespace FCRA.Repository.SeedData
{
    internal static class CommonMasterData
    {
        public static void SeedCommonMasterData(this ModelBuilder builder)
        {
            DateTime createdOn = new(Constants.CreatedOnYear, Constants.CreatedOnMonth, Constants.CreatedOnDay);

            builder.Entity<UserType>().HasData(
                    new UserType { Id = 1, Name = "Super Admin" }
                    , new UserType { Id = 2, Name = "Admin" }
                    , new UserType { Id = 3, Name = "User" }
                    , new UserType { Id = 4, Name = "Reviewer" }
                    , new UserType { Id = 5, Name = "Approver" }
                );

            builder.Entity<RoleMaster>().HasData(
                new RoleMaster { Id = 1, Name = "Super Admin", IsActive = true, UserTypeId = 1, CreatedBy = Constants.UserId, CreatedOn = createdOn }
                );

            List<RolePermissions> rolePermissions = new();
            foreach (var item in Enum.GetValues(typeof(FormDefination)).Cast<FormDefination>())
            {
                rolePermissions.Add(new RolePermissions
                {
                    RoleId = 1,
                    FormId = (int)item,
                    FormName = item.GetDisplayName(),
                    View = true,
                    Add = true,
                    Edit = true
                });
            }
            builder.Entity<RolePermissions>().HasData(rolePermissions);

            builder.Entity<UserMaster>().HasData(new UserMaster
            {
                Id = 1,
                Name = "Super Admin",
                Email = "admin@admin.com",
                Password = EncriptorUtility.Encrypt("Admin@123", false),
                IsActive = true,
                CreatedBy = Constants.UserId,
                CreatedOn = createdOn
            });
            builder.Entity<UserRoles>().HasData(
                    new UserRoles { RoleId = 1, UserId = Constants.UserId }
                );

            //Default scale
            builder.Entity<DefaultScale>().HasData(
                   //3 point
                   new DefaultScale { Id = 1, ScaleType = ScaleType.ThreePoint, RankType = RankType.Rank3, Name = "Weak" }
                   , new DefaultScale { Id = 2, ScaleType = ScaleType.ThreePoint, RankType = RankType.Rank2, Name = "Adequate" }
                   , new DefaultScale { Id = 3, ScaleType = ScaleType.ThreePoint, RankType = RankType.Rank1, Name = "Needs Improvement" }
                   //4 point
                   , new DefaultScale { Id = 4, ScaleType = ScaleType.FourPoint, RankType = RankType.Rank4, Name = "Weak" }
                   , new DefaultScale { Id = 5, ScaleType = ScaleType.FourPoint, RankType = RankType.Rank3, Name = "Adequate" }
                   , new DefaultScale { Id = 6, ScaleType = ScaleType.FourPoint, RankType = RankType.Rank2, Name = "Needs Improvement" }
                   , new DefaultScale { Id = 7, ScaleType = ScaleType.FourPoint, RankType = RankType.Rank1, Name = "Strong" }
                   //5 point
                   , new DefaultScale { Id = 8, ScaleType = ScaleType.FivePoint, RankType = RankType.Rank5, Name = "Absent" }
                   , new DefaultScale { Id = 9, ScaleType = ScaleType.FivePoint, RankType = RankType.Rank4, Name = "Weak" }
                   , new DefaultScale { Id = 10, ScaleType = ScaleType.FivePoint, RankType = RankType.Rank3, Name = "Adequate" }
                   , new DefaultScale { Id = 11, ScaleType = ScaleType.FivePoint, RankType = RankType.Rank2, Name = "Needs Improvement" }
                   , new DefaultScale { Id = 12, ScaleType = ScaleType.FivePoint, RankType = RankType.Rank1, Name = "Strong" }
               );

            //Approval status
            builder.Entity<ApprovalStatus>().HasData(
                    new ApprovalStatus { Id = 1, Name = "Submitted for Review" }
                    , new ApprovalStatus { Id = 2, Name = "Sent back by Reviewer" }
                    , new ApprovalStatus { Id = 3, Name = "Submitted for Approval" }
                    , new ApprovalStatus { Id = 4, Name = "Sent back by Approver" }
                    , new ApprovalStatus { Id = 5, Name = "Approved" }
                );

            //Configuration status
            builder.Entity<CustomerConfiguration>().HasData(
                    new CustomerConfiguration { Id = 1, FieldId = 8, FieldName = "Stage", Visible = false }
                    , new CustomerConfiguration { Id = 2, FieldId = 9, FieldName = "Risk Type", Visible = false }
                    , new CustomerConfiguration { Id = 3, FieldId = 10, FieldName = "Geographic Presence", Visible = false }
                    , new CustomerConfiguration { Id = 4, FieldId = 11, FieldName = "Business Segment", Visible = false }
                    , new CustomerConfiguration { Id = 5, FieldId = 12, FieldName = "Sub Unit", Visible = false }
                );
        }
    }
}
