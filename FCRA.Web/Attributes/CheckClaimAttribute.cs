using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Policy;

namespace FCRA.Web
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CheckClaimAttribute : ActionFilterAttribute
    {
        private readonly string _ClaimName = string.Empty;
        public CheckClaimAttribute(string claimName)
        {
            _ClaimName = claimName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (string.IsNullOrWhiteSpace(_ClaimName))
                return;
            var user = context.HttpContext.User;
            var userCustomerId = user?.Claims?.Where(t => t.Type == _ClaimName)?.Select(t => t.Value)?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(userCustomerId))
            {
                string returnURL = string.Empty;
                string? currentActionName = Convert.ToString(context.RouteData?.Values["action"]);
                string? currentController = Convert.ToString(context.RouteData?.Values["controller"]);
                string? currentArea = Convert.ToString(context.RouteData?.Values["area"]);
                if (!string.IsNullOrWhiteSpace(currentActionName) && !string.IsNullOrWhiteSpace(currentController))
                    returnURL = $"{(string.IsNullOrWhiteSpace(currentArea) ? "~" : $"~/{currentArea}")}/{currentController}/{currentActionName}";
                context.Result = new RedirectResult($"~/Auth/CustomerSelection?ReturnUrl={returnURL}");
                return;
            }
        }
    }
}
