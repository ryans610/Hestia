using System.Collections;

namespace RyanJuan.Hestia.Resources;

internal sealed class UnknownTypeEqualityComparer : IEqualityComparer
{
    public static UnknownTypeEqualityComparer Default { get; } = new();

    private UnknownTypeEqualityComparer() { }

    public new bool Equals(object? x, object? y)
    {
        if (ReferenceEquals(x, y))
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
