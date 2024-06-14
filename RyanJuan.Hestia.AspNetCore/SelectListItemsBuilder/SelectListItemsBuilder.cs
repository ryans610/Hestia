namespace RyanJuan.Hestia.AspNetCore;

public static class SelectListItemsBuilder
{
    [PublicAPI]
    public static ISelectListItemsBuilder<TSource> From<TSource>(
        IEnumerable<TSource> source)
    {
        return new StandardSelectListItemsBuilder<TSource>(source);
    }
}
