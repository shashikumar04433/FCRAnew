using FCRA.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Masters
{
    public class CustomerVersionMasterViewModel : BaseViewModel
    {
        public int CustomerId { get; set; }
        public string? VersionName { get; set; }
    }
}
