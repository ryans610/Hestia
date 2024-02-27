using AutoMapper;

using System.Text.RegularExpressions;

namespace RyanJuan.Hestia.AspNetCore.AutoMappers;

/// <summary>
/// The naming convention with upper character and underscore as separator for AutoMapper.
/// </summary>
public class UpperUnderscoreNamingConvention : INamingConvention
{
    /// <summary>
    /// Default instance of <see cref="UpperUnderscoreNamingConvention"/>
    /// </summary>
    public static readonly UpperUnderscoreNamingConvention Instance = new();

    /// <inheritdoc />
    public Regex SplittingExpression => LowerUnderscoreNamingConvention.Instance.SplittingExpression;

    /// <inheritdoc />
    public string SeparatorCharacter => LowerUnderscoreNamingConvention.Instance.SeparatorCharacter;

    /// <inheritdoc />
    public string ReplaceValue(Match match)
    {
        return match.Value.ToUpper();
    }
}
