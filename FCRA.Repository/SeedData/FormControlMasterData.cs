using FCRA.Common;
using FCRA.Common.FormControls;
using FCRA.Models.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository.SeedData
{
    internal static class FormControlMasterData
    {
        public static void SeedFormControlMasterData(this ModelBuilder builder)
        {
            DateTime createdOn = new(Constants.CreatedOnYear, Constants.CreatedOnMonth, Constants.CreatedOnDay);

            List<FormControlMaster> formControlMasters = new();
            //Enquiry
            //foreach (var item in Enum.GetValues(typeof(Enquiry)).Cast<Enquiry>())
            //{
            //    formControlMasters.Add(new()
            //    {
            //        FormId = (int)FormDefination.Enquiry,
            //        ControlId = (int)item,
            //        ControlName = item.GetDisplayName(),
            //        IsVisible = true
            //    });
            //}
            builder.Entity<FormControlMaster>().HasData(formControlMasters);

        }
    }
}
