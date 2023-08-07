using FCRA.Common;
using FCRA.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Models.Defaults
{
    public class DefaultScale : BaseIdModel
    {
        public ScaleType ScaleType { get; set; }
        public RankType RankType { get; set; }
        [Required, StringLength(50)]
        public string? Name { get; set; }
    }
}
