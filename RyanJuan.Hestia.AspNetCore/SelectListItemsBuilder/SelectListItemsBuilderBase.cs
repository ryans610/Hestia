using System.Collections;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace RyanJuan.Hestia.AspNetCore;

internal class SelectListItemsBuilderBase<TSource>(
    ISelectListItemsBuilder<TSource> next) : ISelectListItemsBuilder<TSource>
{
    internal Func<TSource?, string>? _textSelector;
    internal Func<TSource?, string>? _valueSelector;
    internal bool _useCurrent = false;
    internal TSource? _current;
    internal IEqualityComparer<TSource> _comparer = EqualityComparer<TSource>.Default;
    internal bool _useEmptyDefaultOption = false;
    internal string? _emptyDefaultOptionText;
    internal string? _emptyDefaultOptionValue;
    internal Predicate<TSource?>? _disabledPredicate;

    public IEnumerable<SelectListItem> Build()
    {
        return BuildWithSource().Select(x => x.Item);
    }

    public virtual IEnumerable<(SelectListItem Item, TSource? Source)> BuildWithSource()
    {
        var items = next
            .BuildWithSource()
            .Select(ProcessSelectListItem);
        items = SetEmptyDefaultOption(items);
        return items;
    }

    internal (SelectListItem Item, TSource? Source) ProcessSelectListItem((SelectListItem Item, TSource? Source) tuple)
    {
        var (item, source) = tuple;

        if (_textSelector is not null)
        {
            item.Text = _textSelector.Invoke(source);
        }

        if (_valueSelector is not null)
        {
            item.Value = _valueSelector.Invoke(source);
        }

        if (_useCurrent)
        {
            item.Selected = _comparer.Equals(source, _current);
        }

        if (_disabledPredicate is not null)
        {
            item.Disabled = _disabledPredicate.Invoke(source);
        }

        return (item, source);
    }

    internal IEnumerable<(SelectListItem Item, TSource? Source)> SetEmptyDefaultOption(
        IEnumerable<(SelectListItem Item, TSource? Source)> items)
    {
        return _useEmptyDefaultOption
            ? items.Prepend((new(_emptyDefaultOptionText, _emptyDefaultOptionValue), default))
            : items;
    }

    internal virtual SelectListItemsBuilderBase<TSource> Copy(SelectListItemsBuilderBase<TSource>? instance = null)
    {
        instance ??= new(next);
        instance._textSelector = _textSelector;
        instance._valueSelector = _valueSelector;
        instance._useCurrent = _useCurrent;
        instance._current = _current;
        instance._comparer = _comparer;
        instance._useEmptyDefaultOption = _useEmptyDefaultOption;
        instance._emptyDefaultOptionText = _emptyDefaultOptionText;
        instance._emptyDefaultOptionValue = _emptyDefaultOptionValue;
        instance._disabledPredicate = _disabledPredicate;
        return instance;
    }

    public IEnumerator<SelectListItem> GetEnumerator() => Build().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
