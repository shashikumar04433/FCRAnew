using FCRA.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }
        public int GetUserId()
        {
            var userId = User?.Claims?.Where(t => t.Type == "userId")?.Select(t => t.Value)?.FirstOrDefault();
            return string.IsNullOrWhiteSpace(userId) ? -1 : Convert.ToInt32(userId);
        }

        public int GetUserType()
        {
            var userType = User?.Claims?.Where(t => t.Type == "userType")?.Select(t => t.Value)?.FirstOrDefault();
            return string.IsNullOrWhiteSpace(userType) ? -1 : Convert.ToInt32(userType);
        }

        public int GetUserCustomerId()
        {
            var userCustomerId = User?.Claims?.Where(t => t.Type == "userCustomerId")?.Select(t => t.Value)?.FirstOrDefault();
            return string.IsNullOrWhiteSpace(userCustomerId) ? -1 : Convert.ToInt32(userCustomerId);
        }
        public string? GetUserCustomerName()
        {
            return User?.Claims?.Where(t => t.Type == "userCustomerName")?.Select(t => t.Value)?.FirstOrDefault();
        }
        public int GetUserCustomerScale()
        {
            var userCustomerScale = User?.Claims?.Where(t => t.Type == "customerScaleType")?.Select(t => t.Value)?.FirstOrDefault();
            return string.IsNullOrWhiteSpace(userCustomerScale) ? 3 : Convert.ToInt32(userCustomerScale);
        }
        public bool IsTreeViewType()
        {
            var isTree = User?.Claims?.Where(t => t.Type == "isTree")?.Select(t => t.Value)?.FirstOrDefault();
            if (Int32.TryParse(isTree, out int isTreeInt) && isTreeInt == 1)
                return true;
            return false;
        }

        public void SetApplicationResult(bool status, string message)
        {
            TempData.Put<AppResultViewModel>("AppResult", new AppResultViewModel()
            {
                Status = status,
                Message = message
            });
        }
    }
}
