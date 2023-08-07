using FCRA.Repository.Managers;
using FCRA.ViewModels.Masters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FCRA.Web.Areas.Admin.Controllers
{
    [ValidateFormAccess(Common.FormDefination.Country)]
    [Area("Admin")]
    public class CountryController : MastersController<CountryViewModel>
    {
        public CountryController(IMasterManager<CountryViewModel> manager)
            : base(manager, "Country", "Country", null, null)
        {
        }

    }
}
