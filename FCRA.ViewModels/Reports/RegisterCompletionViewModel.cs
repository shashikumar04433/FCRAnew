using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Reports
{
    public class RegisterCompletionViewModel
    {
        public int Id { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalCompleted { get; set; }
        public int TotalPercentage { get; set; }
    }
}
