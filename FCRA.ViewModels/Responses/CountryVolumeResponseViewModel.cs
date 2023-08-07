using FCRA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Responses
{
    public class CountryVolumeResponseViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public RiskRating RiskRating { get; set; }
        public decimal? Volume { get; set; }
    }
}
