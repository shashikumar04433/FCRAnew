using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace FCRA.Web
{
    [HtmlTargetElement("label", Attributes = ForAttributeName)]
    public class LabelRequiredTagHelper : LabelTagHelper
    {
        private const string ForAttributeName = "asp-for";
        public LabelRequiredTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            var existingCssClassValue = output.Attributes.FirstOrDefault(x => x.Name == "class")?.Value.ToString();
            output.AddClass("form-label", HtmlEncoder.Default);
            if (For.Metadata.IsRequired)
            {
                if (string.IsNullOrWhiteSpace(existingCssClassValue) || !existingCssClassValue.Contains("nomandatory", StringComparison.InvariantCultureIgnoreCase))
                {
                    //var sup = new TagBuilder("sup");
                    //sup.AddCssClass("mandatory-label");
                    //sup.InnerHtml.Append("*");
                    //output.Content.AppendHtml(sup);                   
                    output.AddClass("mend", HtmlEncoder.Default);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(existingCssClassValue) && existingCssClassValue.Contains("mandatory", StringComparison.InvariantCultureIgnoreCase))
                {
                    //var sup = new TagBuilder("sup");
                    //sup.AddCssClass("mandatory-label");
                    //sup.InnerHtml.Append("*");
                    //output.Content.AppendHtml(sup);
                    //output.AddClass("form-label", HtmlEncoder.Default);
                }
            }
        }
    }
}
