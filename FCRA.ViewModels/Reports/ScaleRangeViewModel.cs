using FCRA.ViewModels.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Reports
{
    public class ScaleRangeViewModel
    {
        public string? Name { get; set; }
        public decimal ScaleRange2 { get; set;}
        public decimal ScaleRange3 { get; set;}
        public decimal? ScaleRange4 { get; set;}
        public decimal? ScaleRange5 { get; set;}
        public static ScaleRangeViewModel GetScale(StageViewModel request)
        {
            return new ScaleRangeViewModel()
            {
                Name = request.Name,
                ScaleRange2=request.ScaleRange2,
                ScaleRange3=request.ScaleRange3,
                ScaleRange4=request.ScaleRange4,
                ScaleRange5= request.ScaleRange5,
            };
        }
        public static ScaleRangeViewModel GetScale(RiskTypeViewModel request)
        {
            return new ScaleRangeViewModel()
            {
                Name = request.Name,
                ScaleRange2 = request.ScaleRange2,
                ScaleRange3 = request.ScaleRange3,
                ScaleRange4 = request.ScaleRange4,
                ScaleRange5 = request.ScaleRange5,
            };
        }
        public static ScaleRangeViewModel GetScale(GeographicPresenceViewModel request)
        {
            return new ScaleRangeViewModel()
            {
                Name = request.Name,
                ScaleRange2 = request.ScaleRange2,
                ScaleRange3 = request.ScaleRange3,
                ScaleRange4 = request.ScaleRange4,
                ScaleRange5 = request.ScaleRange5,
            };
        }
        public static ScaleRangeViewModel GetScale(CustomerSegmentViewModel request)
        {
            return new ScaleRangeViewModel()
            {
                Name = request.Name,
                ScaleRange2 = request.ScaleRange2,
                ScaleRange3 = request.ScaleRange3,
                ScaleRange4 = request.ScaleRange4,
                ScaleRange5 = request.ScaleRange5,
            };
        }
        public static ScaleRangeViewModel GetScale(BusinessSegmentViewModel request)
        {
            return new ScaleRangeViewModel()
            {
                Name = request.Name,
                ScaleRange2 = request.ScaleRange2,
                ScaleRange3 = request.ScaleRange3,
                ScaleRange4 = request.ScaleRange4,
                ScaleRange5 = request.ScaleRange5,
            };
        }
    }
    
}
