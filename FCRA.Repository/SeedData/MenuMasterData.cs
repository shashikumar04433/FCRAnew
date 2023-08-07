using FCRA.Models.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.SeedData
{
    internal static class MenuMasterData
    {
        public static void SeedMenuMasterData(this ModelBuilder builder)
        {
            DateTime CreatedOn = new (Constants.CreatedOnYear, Constants.CreatedOnMonth, Constants.CreatedOnDay);
            builder.Entity<MenuMaster>().HasData(
                //Top level menus
                    new MenuMaster() { Id = 1, Name = "Masters", IconClass = "fas fa-layer-group", CreatedBy = Constants.UserId, CreatedOn = CreatedOn, Sequence = 1, IsActive = true, IsAdmin = true }
                    , new MenuMaster() { Id = 2, Name = "User Management", IconClass = "fas fa-users", CreatedBy = Constants.UserId, CreatedOn = CreatedOn, Sequence = 2, IsActive = true, IsAdmin = true }
                    //, new MenuMaster() { Id = 6, Name = "Audit Log", IconClass = "fas fa-clipboard-list", CreatedBy = Constants.UserId, CreatedOn = CreatedOn, Sequence = 3, IsActive = true, IsAdmin = false }

                    //Sub level menues
                    , new MenuMaster() { Id = 4, Name = "Customer Details", IconClass = "fas fa-user-graduate", CreatedBy = Constants.UserId, CreatedOn = CreatedOn, Sequence = 1, IsActive = true, IsAdmin = true, ParentMenuId = 1 }
                    , new MenuMaster() { Id = 3, Name = "Mappings", IconClass = "fas fa-th-list", CreatedBy = Constants.UserId, CreatedOn = CreatedOn, Sequence = 2, IsActive = true, IsAdmin = true, ParentMenuId = 1 }
                    //, new MenuMaster() { Id = 5, Name = "Risk Assessment", IconClass = "fas fa-task-alt", CreatedBy = Constants.UserId, CreatedOn = CreatedOn, Sequence = 3, IsActive = true, IsAdmin = true, ParentMenuId = 1 }

            );
        }
    }
}

