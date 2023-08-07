using FCRA.ViewModels.Account;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FCRA.Web
{
    public static class ViewContextExtension
    {
        public static FormControlListViewModel GetControlPermissions(this ViewContext viewContext)
        {
            FormControlListViewModel model = new();
            if (viewContext.ViewBag.ControlPermissions != null)
                model = (FormControlListViewModel)viewContext.ViewBag.ControlPermissions;
            return model;
        }
    }
}
