using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace FCRA.Web
{
    [HtmlTargetElement("input", Attributes = ForAttributeName)]
    public class InputRequiredTagHelper : InputTagHelper
    {
        private const string ForAttributeName = "asp-for";
        public InputRequiredTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            var existingCssClassValue = output.Attributes.FirstOrDefault(x => x.Name == "class")?.Value.ToString();
            if (existingCssClassValue == null || (!existingCssClassValue.Contains("form-check-input") && !existingCssClassValue.Contains("form-radio-input")))
                output.AddClass("form-control", HtmlEncoder.Default);
        }
    }
}
