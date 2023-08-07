using FCRA.Common;
using FCRA.Repository.Managers;
using FCRA.ViewModels;
using FCRA.ViewModels.Customers;
using FCRA.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;

namespace FCRA.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAccountManager _manager;
        private readonly IMasterManager<CustomerViewModel> _customerManager;
        private readonly StorageSettings _storageSettings;
        public AuthController(IAccountManager manager, IOptions<StorageSettings> options
            , IMasterManager<CustomerViewModel> customerManager)
        {
            _manager = manager;
            _customerManager = customerManager;
            _storageSettings = options.Value;
        }
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View("LoginEmail");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string emailaddress, LoginViewModel? model)
        {
            ViewBag.ReturnUrl = model?.ReturnUrl;

            if (string.IsNullOrEmpty(emailaddress))
                return View("LoginEmail");
            ViewBag.Email = emailaddress;

            var userDetails = await _manager.GetUserByEmail(emailaddress);
            if (userDetails == null || string.IsNullOrWhiteSpace(userDetails.Email) || !userDetails.IsActive)
            {
                ViewBag.ErrorMessage = "Email does not exists";
                return View("LoginEmail", emailaddress);
            }

            if (model == null || string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.ClearValidationState(nameof(LoginViewModel.Password));
                return View("Login", new LoginViewModel() { Email = emailaddress, ReturnUrl = model?.ReturnUrl });
            }
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }
            var user = await _manager.Login(model!.Email!, model.Password);
            if (user == null || string.IsNullOrWhiteSpace(user.Email))
            {
                ModelState.AddModelError(nameof(LoginViewModel.Password), "Invalid credentials");
                return View("Login", model);
            }

            await IdentitySignin(user.Email, user.Id!, user.Name!, model.RememberMe, user.Role?.Name
                , null, user.Customer?.Name, user.Role?.UserTypeId ?? -1, 0, user.CustomerId ?? 0
                , user.Customer?.ScaleType ?? Common.ScaleType.ThreePoint);

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                return Redirect(model.ReturnUrl);
            //return RedirectToAction("Index", "Home", new { area = "" });
            return RedirectToAction("Index", "RiskAssessment", new { area = "" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="isPersistent"></param>
        /// <param name="Role"></param>
        /// <param name="companyLogo"></param>
        /// <param name="companyName"></param>
        /// <param name="isTree">1=Tree Type, 2=Normal</param>
        /// <returns></returns>
        private async Task IdentitySignin(string userName, int userId, string name, bool isPersistent = false, string? Role = null, string? companyLogo = null, string? customerName = null, int userType = -1, int isTree = 0, int customerId = 0, ScaleType customerScaleType = Common.ScaleType.ThreePoint)
        {
            var claims = new List<Claim>
                {
                    new Claim("user", userName),
                    new Claim("userId",Convert.ToString( userId)),
                    new Claim("userName", name),
                    new Claim("isTree", Convert.ToString(isTree)),
                    new Claim("IsPersistent", Convert.ToString(isPersistent)),
                    new Claim("userType", Convert.ToString(userType))
                };
            if (!string.IsNullOrWhiteSpace(Role))
                claims.Add(new("role", Role));
            else
                claims.Add(new("role", "User"));
            if (!string.IsNullOrWhiteSpace(companyLogo))
            {
                claims.Add(new("logourl", companyLogo));
            }
            if (!string.IsNullOrWhiteSpace(customerName))
            {
                claims.Add(new("userCustomerName", customerName));
            }
            if (customerId > 0)
            {
                claims.Add(new("userCustomerId", Convert.ToString(customerId)));
                claims.Add(new("customerScaleType", Convert.ToString((int)customerScaleType)));
            }
            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme, "user", "role")), new AuthenticationProperties() { IsPersistent = isPersistent });
        }

        public async Task<IActionResult> LogOff()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserPermissions");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> CustomerSelection(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            var list = await _customerManager.GetAsync();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerSelection(int customerId, int customerScale, string customerName, string? ReturnUrl)
        {
            var principle = (User as System.Security.Claims.ClaimsPrincipal);
            var claimsIdentity = (ClaimsIdentity)principle.Identity!;
            if (User?.Claims?.FirstOrDefault(t => t.Type == "userCustomerId") != null)
            {
                var cIdClaim = User.Claims.First(t => t.Type == "userCustomerId");
                var cNameClaim = User.Claims.First(t => t.Type == "userCustomerName");
                var cScaleClaim = User.Claims.First(t => t.Type == "customerScaleType");
                claimsIdentity.RemoveClaim(cIdClaim);
                claimsIdentity.RemoveClaim(cNameClaim);
                claimsIdentity.RemoveClaim(cScaleClaim);
            }

            claimsIdentity.AddClaim(new Claim("userCustomerId", Convert.ToString(customerId)));
            claimsIdentity.AddClaim(new Claim("userCustomerName", customerName));
            claimsIdentity.AddClaim(new Claim("customerScaleType", Convert.ToString(customerScale)));
            Claim? claim = ((ClaimsIdentity)User!.Identity!).FindFirst("IsPersistent");
            bool IsPersistent = claim != null ? Convert.ToBoolean(claim.Value) : false;
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties() { IsPersistent = IsPersistent });

            if (!string.IsNullOrWhiteSpace(ReturnUrl))
                return Redirect(ReturnUrl);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
