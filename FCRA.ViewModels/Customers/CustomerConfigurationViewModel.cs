using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Customers
{
    public class CustomerConfigurationViewModel
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        [Required, MaxLength(50)]
        public string? FieldName { get; set; }
        public bool Visible { get; set; }
        
    }
}
