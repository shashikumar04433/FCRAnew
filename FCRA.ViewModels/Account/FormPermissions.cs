using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels.Account
{
    public class FormPermissions
    {
        public bool View { get; set; } = false;
        public bool Add { get; set; } = false;
        public bool Edit { get; set; } = false;

        public string EditViewText { get { return Edit ? "Edit" : "View"; } }
        public string EditViewIconClass { get { return Edit ? "fas fa-edit text-warning" : "fas fa-eye"; } }
        public bool CanShowAddEditButton(int id)
        {
            return Edit || (id == 0 && Add);
        }
    }
}
