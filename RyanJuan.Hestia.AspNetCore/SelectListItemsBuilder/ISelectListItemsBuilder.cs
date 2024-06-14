using Microsoft.AspNetCore.Mvc.Rendering;

namespace RyanJuan.Hestia.AspNetCore;

public interface ISelectListItemsBuilder<TSource> : IEnumerable<SelectListItem>
{
    IEnumerable<SelectListItem> Build();
    IEnumerable<(SelectListItem Item, TSource? Source)> BuildWithSource();
}
