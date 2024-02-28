using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace RyanJuan.Hestia.AspNetCore.TagHelpers;

/// <summary>
/// Display validation message in bootstrap 5 format when a "div" or "span" element has "asp-bootstrap-validation-for" attribute.
/// </summary>
/// <inheritdoc cref="BootstrapValidationMessageTagHelper"/>
[HtmlTargetElement("div", Attributes = ValidationForAttributeName)]
[HtmlTargetElement("span", Attributes = ValidationForAttributeName)]
public class BootstrapValidationMessageTagHelper(IHtmlGenerator generator) : TagHelper
{
    private const string DataValidationForAttributeName = "data-valmsg-for";
    private const string DataValidationReplaceAttributeName = "data-valmsg-replace";
    private const string ValidationForAttributeName = "asp-bootstrap-validation-for";

    /// <inheritdoc />
    public override int Order => -1000;

    /// <summary>
    /// Gets the <see cref="Microsoft.AspNetCore.Mvc.Rendering.ViewContext"/> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = null!;

    /// <summary>
    /// Gets the <see cref="IHtmlGenerator"/> used to generate the <see cref="BootstrapValidationMessageTagHelper"/>'s output.
    /// </summary>
    protected IHtmlGenerator Generator { get; } = generator;

    /// <summary>
    /// Gets an expression to be evaluated against the current model.
    /// </summary>
    [HtmlAttributeName(ValidationForAttributeName)]
    public ModelExpression For { get; set; } = null!;

    /// <inheritdoc />
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(For, ValidationForAttributeName);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(output);

        ViewContext.ValidationMessageElement = context.TagName;
        // Ensure Generator does not throw due to empty "fullName" if user provided data-valmsg-for attribute.
        // Assume data-valmsg-for value is non-empty if attribute is present at all. Should align with name of
        // another tag helper e.g. an <input/> and those tag helpers bind Name.
        IDictionary<string, object>? htmlAttributes = null;
        if (string.IsNullOrEmpty(For.Name) &&
            string.IsNullOrEmpty(ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix) &&
            output.Attributes.ContainsName(DataValidationForAttributeName))
        {
            htmlAttributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { DataValidationForAttributeName, "-non-empty-value-" },
            };
        }

        string? message = null;
        if (!output.IsContentModified)
        {
            var tagHelperContent = await output.GetChildContentAsync();

            // We check for whitespace to detect scenarios such as:
            // <span validation-for="Name">
            // </span>
            if (!tagHelperContent.IsEmptyOrWhiteSpace)
            {
                message = tagHelperContent.GetContent();
            }
        }

        var tagBuilder = Generator.GenerateValidationMessage(
            ViewContext,
            For.ModelExplorer,
            For.Name,
            message: message,
            tag: null,
            htmlAttributes: htmlAttributes);

        if (tagBuilder is not null)
        {
            output.AddClass("invalid-feedback", HtmlEncoder.Default);
            output.MergeAttributes(tagBuilder);

            // Do not update the content if another tag helper targeting this element has already done so.
            if (!output.IsContentModified && tagBuilder.HasInnerHtml)
            {
                output.Content.SetHtmlContent(tagBuilder.InnerHtml);
            }
        }
    }
}
