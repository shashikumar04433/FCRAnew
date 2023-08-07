using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Account
{
    public class RolePermissionsViewModel
    {
        public int RoleId { get; set; }
        public int FormId { get; set; }
        [Required, MaxLength(50)]
        public string? FormName { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public string? FormNameLabel { get; set; }
    }
}
