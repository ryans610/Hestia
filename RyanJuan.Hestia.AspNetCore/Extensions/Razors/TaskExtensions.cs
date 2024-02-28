using JetBrains.Annotations;

using Microsoft.AspNetCore.Html;

namespace RyanJuan.Hestia.AspNetCore.Extensions.Razors;

/// <summary>
/// 
/// </summary>
public static class HestiaAspNetCoreTaskExtensions
{
    /// <summary>
    /// For easily using await Task in razor.
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    [PublicAPI]
    public static async Task<HtmlString> RenderNothing(this Task task)
    {
        ArgumentNullException.ThrowIfNull(task);
        await task;
        return HtmlString.Empty;
    }
}
