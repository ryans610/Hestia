//using System.Collections;

//namespace RyanJuan.Hestia.Concurrent;

//[PublicAPI]
//public class ConcurrentHashCollection<T>:
//    ICollection<T>,
//#if NETCOREAPP || NETSTANDARD || NET45_OR_GREATER
//    IReadOnlyCollection<T>,
//#endif
//    IEnumerable<T>
//{


//    internal ConcurrentHashCollection(IEqualityComparer<T>? comparer)
//    {
//        if (comparer is null ||
//            ReferenceEquals(comparer, EqualityComparer<T>.Default))
//        {
//            _isDefaultComparer = true;
//            _data = new DataContainer();
//        }
//        else
//        {
//            _isDefaultComparer = false;

//        }
//    }

//    private readonly bool _isDefaultComparer;
//    private DataContainer _data;
    
//    /// <inheritdoc cref="ICollection{T}.Count" />
//    public int Count
//    {
//        get
//        {
//            var data = _data;
//            if (data._hasNull)
//            {
//                return data.
//            }
//            return _hasNull ? _collection.Count + 1 : _collection.Count;
//        }
//    }

//    bool ICollection<T>.IsReadOnly => false;

//    public IEnumerator<T> GetEnumerator()
//    {
//        throw new NotImplementedException();
//    }

//    public bool Contains(T item)
//    {
//        return item is null ? _hasNull : _collection.ContainsKey(item);
//    }

//    public void Add(T item)
//    {
//        if (item is not null)
//        {
//            _collection.TryAdd(item, null);
//        }

//    }

//    public bool Remove(T item)
//    {
//        throw new NotImplementedException();
//    }

//    public void Clear()
//    {
//        if (_isDefaultComparer)
//        {
//            _data = new DataContainer();
//        }
//        else
//        {

//        }
//    }

//    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
//    {
//        throw new NotImplementedException();
//    }

//    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

//    private class DataContainer
//    {
//        private readonly ConcurrentDictionary<T, object?> _collection = new();
//        public volatile bool _hasNull = false;

//    }
//}
