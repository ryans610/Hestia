using System;
using System.Collections;

namespace RyanJuan.Hestia.NonGeneric
{
    public static partial class HestiaNonGenericCollections
    {
#if ZH_HANT
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
#endif
        public static bool All(
            this IEnumerable source,
            Func<object?, bool> predicate)
        {
            Error.ThrowIfArgumentNull(nameof(source), source);
            var result = true;
            var iterator = source.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (!predicate(iterator.Current))
                {
                    result = false;
                    break;
                }
            }
            if (iterator is IDisposable disposable)
            {
                disposable.Dispose();
            }
            return result;
        }
    }
}
