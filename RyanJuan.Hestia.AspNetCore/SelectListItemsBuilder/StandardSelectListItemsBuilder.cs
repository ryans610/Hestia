using Microsoft.AspNetCore.Mvc.Rendering;

namespace RyanJuan.Hestia.AspNetCore;

internal class StandardSelectListItemsBuilder<TSource>(IEnumerable<TSource> source) :
    SelectListItemsBuilderBase<TSource>(null!)
{
    public override IEnumerable<(SelectListItem Item, TSource? Source)> BuildWithSource()
    {
        var items = source
            .Select(x => (new SelectListItem(), x))
            .Select(ProcessSelectListItem);
        items = SetEmptyDefaultOption(items);
        return items;
    }

    internal override SelectListItemsBuilderBase<TSource> Copy(SelectListItemsBuilderBase<TSource>? instance = null)
    {
        instance ??= new StandardSelectListItemsBuilder<TSource>(source);
        return base.Copy(instance);
    }
}
