using Microsoft.AspNetCore.Mvc;

namespace RyanJuan.Hestia.AspNetCore.Extensions;

public static class UrlHelperExtensions
{
    public static string? AbsoluteAction(
        this IUrlHelper urlHelper,
        string action,
        string controller,
        object? values = null,
        string? protocol = null)
    {
        return urlHelper.Action(
            action,
            controller,
            values,
            protocol ?? urlHelper.ActionContext.HttpContext.Request.Scheme);
    }

    public static string? AbsoluteActionHttps(
        this IUrlHelper urlHelper,
        string action,
        string controller,
        object? values = null)
    {
        return AbsoluteAction(urlHelper, action, controller, values, "https");
    }
}
