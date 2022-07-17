using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// Check if the value is not the default value of the type.
/// </summary>
public class NotDefaultAttribute : ValidationAttribute
{
    /// <inheritdoc cref="NotDefaultAttribute"/>
    public NotDefaultAttribute() { }

    /// <inheritdoc />
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var defaultValue = validationContext.ObjectType.GetDefaultValue();
        if (UnknownTypeEqualityComparer.Default.Equals(value, defaultValue))
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
        return ValidationResult.Success!;
    }


    // TODO: Change to RyanJuan.Hestia.NonGeneric.UnknownTypeEqualityComparer using InternalsVisibleTo
    private sealed class UnknownTypeEqualityComparer : IEqualityComparer
    {
        public static UnknownTypeEqualityComparer Default { get; } = new ();

        private UnknownTypeEqualityComparer() { }

        public new bool Equals(object? x, object? y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            if (x is null || y is null)
            {
                return false;
            }
            return x.Equals(y);
        }

        public int GetHashCode(object obj) => obj?.GetHashCode() ?? default;
    }
}
