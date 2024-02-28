namespace RyanJuan.Hestia;

public static partial class HestiaEnumerable
{
    public static partial class Empty
    {
#if !NET40
        [PublicAPI]
        public static ReadOnlyDictionary<TKey, TValue> EmptyReadOnlyDictionary<TKey, TValue>()
            where TKey : notnull =>
            ReadOnlyDictionaryGenericSource<TKey, TValue>.ReadOnlyDictionary;

        private static class ReadOnlyDictionaryGenericSource<TKey, TValue>
            where TKey : notnull
        {
            public static ReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary { get; } =
                new(new Dictionary<TKey, TValue>());
        }
#endif
    }
}
