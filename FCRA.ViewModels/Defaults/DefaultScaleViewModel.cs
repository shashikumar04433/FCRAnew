using FCRA.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Defaults
{
    public class DefaultScaleViewModel : IdIntViewModel
    {
        [MapToDTO]
        public ScaleType ScaleType { get; set; }
        [MapToDTO]
        public RankType RankType { get; set; }
        [MapToDTO, Required, MaxLength(50)]
        public string? Name { get; set; }
        public virtual DefaultScaleViewModel? DefaultScale { get; set; }
    }
}
