using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace RyanJuan.Hestia.AspNetCore.Extensions;

public static class UrlHelperExtensions
{
    internal const string HttpsProtocol = "https";

    [PublicAPI]
    public static string? AbsoluteAction(
        this IUrlHelper urlHelper,
        string action,
        string controller,
        object? values = null,
        string? protocol = null)
    {
        ArgumentNullException.ThrowIfNull(urlHelper);
        return urlHelper.Action(
            action,
            controller,
            values,
            protocol ?? urlHelper.ActionContext.HttpContext.Request.Scheme);
    }

    [PublicAPI]
    public static string? AbsoluteActionHttps(
        this IUrlHelper urlHelper,
        string action,
        string controller,
        object? values = null)
    {
        return AbsoluteAction(urlHelper, action, controller, values, HttpsProtocol);
    }

    [PublicAPI]
    public static string? AbsoluteRoute(
        this IUrlHelper urlHelper,
        string routeName,
        object? values = null,
        string? protocol = null)
    {
        ArgumentNullException.ThrowIfNull(urlHelper);
        return urlHelper.RouteUrl(
            routeName,
            values,
            protocol ?? urlHelper.ActionContext.HttpContext.Request.Scheme);
    }

    [PublicAPI]
    public static string? AbsoluteRouteHttps(
        this IUrlHelper urlHelper,
        string routeName,
        object? values = null)
    {
        return AbsoluteRoute(urlHelper, routeName, values, HttpsProtocol);
    }
}
