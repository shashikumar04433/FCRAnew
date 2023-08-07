using FCRA.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Masters
{
    [Table(nameof(CustomerVersionMaster))]
    public class CustomerVersionMaster : BaseModel
    {
        public int CustomerId { get; set; }
        public string? VersionName { get; set; }
    }
}


