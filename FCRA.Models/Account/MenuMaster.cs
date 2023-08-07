﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCRA.Models.Base;

namespace FCRA.Models.Account
{
    public class MenuMaster : BaseMasterModel
    {
        [StringLength(100)]
        public string? IconClass { get; set; }
        public int Sequence { get; set; }
        public bool IsAdmin { get; set; }=false;
        public int? ParentMenuId { get; set; }
    }
}
