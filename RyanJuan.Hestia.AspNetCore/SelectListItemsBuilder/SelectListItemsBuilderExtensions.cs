using Microsoft.AspNetCore.Mvc.Rendering;

namespace RyanJuan.Hestia.AspNetCore;

public static class SelectListItemsBuilderExtensions
{
    [PublicAPI]
    public static ISelectListItemsBuilder<TSource> ToSelectListItemsBuilder<TSource>(
        this IEnumerable<TSource> source)
    {
        return SelectListItemsBuilder.From(source);
    }

    [PublicAPI]
    public static ISelectListItemsBuilder<TSource> WithText<TSource>(
        this ISelectListItemsBuilder<TSource> builder,
        Func<TSource?, string> textSelector)
    {
        var builderBase = GetBuilderBase(builder);
        builderBase._textSelector = textSelector;
        return builderBase;
    }

    [PublicAPI]
    public static ISelectListItemsBuilder<TSource> WithValue<TSource>(
        this ISelectListItemsBuilder<TSource> builder,
        Func<TSource?, string> valueSelector)
    {
        var builderBase = GetBuilderBase(builder);
        builderBase._valueSelector = valueSelector;
        return builderBase;
    }

    [PublicAPI]
    public static ISelectListItemsBuilder<TSource> WithValue<TSource>(
        this ISelectListItemsBuilder<TSource> builder,
        Func<TSource?, string> valueSelector,
        TSource? selectedValue,
        IEqualityComparer<TSource>? comparer = null)
    {
        builder = WithValue(builder, valueSelector);
        builder = WithSelectedValue(builder, selectedValue, comparer);
        return builder;
    }

    [PublicAPI]
    public static ISelectListItemsBuilder<TSource> WithSelectedValue<TSource>(
        this ISelectListItemsBuilder<TSource> builder,
        TSource? selectedValue,
        IEqualityComparer<TSource>? comparer = null)
    {
        var builderBase = GetBuilderBase(builder);
        builderBase._useCurrent = true;
        builderBase._current = selectedValue;
        if (comparer is not null)
        {
            builderBase._comparer = comparer;
        }

        return builderBase;
    }

    [PublicAPI]
    public static ISelectListItemsBuilder<TSource> WithEmptyDefaultOption<TSource>(
        this ISelectListItemsBuilder<TSource> builder,
        string emptyDefaultOptionText,
        string emptyDefaultOptionValue = "")
    {
        var builderBase = GetBuilderBase(builder);
        builderBase._useEmptyDefaultOption = true;
        builderBase._emptyDefaultOptionText = emptyDefaultOptionText;
        builderBase._emptyDefaultOptionValue = emptyDefaultOptionValue;
        return builderBase;
    }

    [PublicAPI]
    public static ISelectListItemsBuilder<TSource> WithDisabled<TSource>(
        this ISelectListItemsBuilder<TSource> builder,
        Predicate<TSource?> disabledPredicate)
    {
        var builderBase = GetBuilderBase(builder);
        builderBase._disabledPredicate = disabledPredicate;
        return builderBase;
    }

    //[PublicAPI]
    //public static ISelectListItemsBuilder<TSource> WithGroup<TSource>(
    //    this ISelectListItemsBuilder<TSource> builder,
    //    IReadOnlyList<SelectListGroup> selectListGroupList,
    //    Func<TSource?, SelectListGroup> f)
    //{
    //    var builderBase = GetBuilderBase(builder);

    //    return builderBase;
    //}

    private static SelectListItemsBuilderBase<TSource> GetBuilderBase<TSource>(
        ISelectListItemsBuilder<TSource> builder)
    {
        return builder is SelectListItemsBuilderBase<TSource> builderBase ? builderBase.Copy() : new(builder);
    }
}
