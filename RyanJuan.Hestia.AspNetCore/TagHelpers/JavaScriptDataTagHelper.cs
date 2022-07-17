using Microsoft.AspNetCore.Razor.TagHelpers;

using System.Text.Json;

namespace RyanJuan.Hestia.AspNetCore.TagHelpers;

/// <summary>
/// WARNING!! Will output all the data with json conversion, please make sure all the sensitive data is filtered.
/// <para>
/// Convert the data to javascript's object or array format,
/// and print as javascript's const variable with the specific name.
/// </para>
/// </summary>
[HtmlTargetElement("js-data")]
public class JavaScriptDataTagHelper : TagHelper
{
    [HtmlAttributeName("name")]
    public string JavaScriptDataVariableName { get; set; } = null!; // required from tag

    [HtmlAttributeName("data")]
    public object DataToConvert { get; set; } = null!;  // required from tag

    /// <inheritdoc />
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(JavaScriptDataVariableName, "name");
        if (string.IsNullOrEmpty(JavaScriptDataVariableName))
        {
            // ReSharper disable once NotResolvedInText
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
            // 'paramName' in the constructor of ArgumentException is the attribute name of 'JavaScriptDataVariableName' property when using the tag helper.
            throw new ArgumentException("'name' can not be empty.", "name");
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
        }
        ArgumentNullException.ThrowIfNull(DataToConvert, "data");
        output.TagName = "script";
        output.TagMode = TagMode.StartTagAndEndTag;
        var json = JsonSerializer.Serialize(DataToConvert);
        output.Content.SetHtmlContent($"const {JavaScriptDataVariableName} = {json};");
        await base.ProcessAsync(context, output);
    }
}
