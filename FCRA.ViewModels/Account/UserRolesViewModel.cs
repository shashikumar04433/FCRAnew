﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Account
{
    public class UserRolesViewModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual RoleMasterViewModel? Role { get; set; }
        public string RoleName { get { return Role == null || string.IsNullOrWhiteSpace(Role.Name) ? string.Empty : Role.Name; } }
    }
}
