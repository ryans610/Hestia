using AutoMapper;

#if !NET8_0_OR_GREATER
using System.Text.RegularExpressions;
#endif

using JetBrains.Annotations;

namespace RyanJuan.Hestia.AspNetCore.AutoMappers;

/// <summary>
/// The naming convention with upper character and underscore as separator for AutoMapper.
/// </summary>
[PublicAPI]
public class UpperUnderscoreNamingConvention : INamingConvention
{
    /// <summary>
    /// Default instance of <see cref="UpperUnderscoreNamingConvention"/>
    /// </summary>
    [PublicAPI]
    public static readonly UpperUnderscoreNamingConvention Instance = new();

    /// <inheritdoc />
    [PublicAPI]
    public string SeparatorCharacter => LowerUnderscoreNamingConvention.Instance.SeparatorCharacter;

#if !NET8_0_OR_GREATER
    /// <inheritdoc />
    [PublicAPI]
    public Regex SplittingExpression => LowerUnderscoreNamingConvention.Instance.SplittingExpression;

    /// <inheritdoc />
    [PublicAPI]
    public string ReplaceValue(Match match)
    {
        return match.Value.ToUpper();
    }
#else
    [PublicAPI]
    public string[] Split(string input)
    {
        return LowerUnderscoreNamingConvention.Instance.Split(input);
    }
#endif
}
