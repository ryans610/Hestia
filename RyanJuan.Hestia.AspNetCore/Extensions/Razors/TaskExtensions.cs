using Microsoft.AspNetCore.Html;

namespace RyanJuan.Hestia.AspNetCore.Extensions.Razors;

public static partial class HestiaAspNetCoreTaskExtensions
{
    /// <summary>
    /// For easily using await Task in razor.
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public static async Task<HtmlString> RenderNothing(this Task task)
    {
        await task;
        return HtmlString.Empty;
    }
}
