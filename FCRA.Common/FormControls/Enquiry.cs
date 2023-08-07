using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Common.FormControls
{
    public enum Enquiry : int
    {
        [Display(Name = "Next Followup Date")]
        NextFollowupDate = 1,        
        [Display(Name = "Enquiry Type")]
        EnquiryType = 2,
        [Display(Name = "Category")]
        Category = 3,
        [Display(Name = "Sales Person 1")]
        SalesPerson1 = 4,
        [Display(Name = "Architect")]
        Architect = 5,
        [Display(Name = "Client Name")]
        ClientName = 6,
        [Display(Name = "Project Name")]
        ProjectName = 7,
        [Display(Name = "Amount With Tax")]
        AmountWithTax = 8
    }
}
