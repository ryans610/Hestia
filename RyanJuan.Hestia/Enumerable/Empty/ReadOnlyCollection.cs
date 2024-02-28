namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
    public static partial class Empty
    {
        [PublicAPI]
        public static ReadOnlyCollection<T> ReadOnlyCollection<T>() =>
            ReadOnlyCollectionGenericSource<T>.ReadOnlyCollection;

        private static class ReadOnlyCollectionGenericSource<T>
        {
            public static ReadOnlyCollection<T> ReadOnlyCollection { get; } = new(
#if NETCOREAPP || NETSTANDARD2_0_OR_GREATER || NET48 || NET462
#pragma warning disable IDE0301
                Array.Empty<T>()
#pragma warning restore IDE0301
#else
#pragma warning disable IDE0300
                new T[0]
#pragma warning restore IDE0300
#endif
            );
        }
    }
}
