using JetBrains.Annotations;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// The condition of <see cref="RequiredIfAttribute"/>.
/// </summary>
[PublicAPI]
public enum RequiredIfMode
{
    TargetIsNullOrEmptyString = 0,
    TargetIsNotNullAndNotEmptyString = 1,
    TargetIsNullOrEmptyEnumerable = 2,
    TargetIsNotNullAndNotEmptyEnumerable = 3,
    TargetValueEquals = 4,
    TargetValueNotEquals = 5,
}
