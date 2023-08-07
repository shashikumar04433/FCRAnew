using Microsoft.AspNetCore.Mvc;

namespace FCRA.Web
{
    public class JavaScriptResult:ContentResult
    {
        public JavaScriptResult(string script)
        {
            this.Content = script;
            this.ContentType = "application/javascript";
        }
    }
}
