using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using System.ComponentModel.DataAnnotations;

namespace RyanJuan.Hestia.AspNetCore.TagHelpers;

[HtmlTargetElement("input", Attributes = "[asp-for],[type=number]")]
public class InputNumberRangeToHtmlTagHelper : TagHelper
{
    private const string AspForAttributeName = "asp-for";
    private const string MaxAttributeName = "max";
    private const string MinAttributeName = "min";

    /// <inheritdoc />
    public override int Order => int.MaxValue;

    [HtmlAttributeName(AspForAttributeName)]
    public ModelExpression For { get; set; } = null!;   // required from tag

    /// <inheritdoc />
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await base.ProcessAsync(context, output);
        if (TryGetRangeAttribute(out var rangeAttribute))
        {
            if (!context.AllAttributes.ContainsName(MaxAttributeName))
            {
                output.Attributes.Add(MaxAttributeName, rangeAttribute.Maximum?.ToString() ?? string.Empty);
            }

            if (!context.AllAttributes.ContainsName(MinAttributeName))
            {
                output.Attributes.Add(MinAttributeName, rangeAttribute.Minimum?.ToString() ?? string.Empty);
            }
        }
    }

    private bool TryGetRangeAttribute(out RangeAttribute rangeAttribute)
    {
        foreach (var validator in For.ModelExplorer.Metadata.ValidatorMetadata)
        {
            if (validator is RangeAttribute result)
            {
                rangeAttribute = result;
                return true;
            }
        }
        rangeAttribute = null!; // When fail, the out parameter should not be used.
        return false;
    }
}
