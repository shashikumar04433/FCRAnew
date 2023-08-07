using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Base
{
    public class BaseMasterModel : BaseModel
    {
        [Required, Column(Order = 1), StringLength(100)]
        public virtual string? Name { get; set; }

        [Column(Order = 2), StringLength(400)]
        public virtual string? Description { get; set; }

    }
}
