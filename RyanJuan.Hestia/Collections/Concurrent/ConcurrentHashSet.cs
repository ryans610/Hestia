using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RyanJuan.Hestia.Collections.Concurrent
{
    public class ConcurrentHashSet<T> :
        ISet<T>,
        ICollection<T>,
        IReadOnlyCollection<T>,
        IEnumerable<T>,
        ICollection,
        IEnumerable
        where T : notnull
    {
        #region ctor
        public ConcurrentHashSet()
        {
            _dictionary = new ConcurrentDictionary<T, byte>();
            _comparer = EqualityComparer<T>.Default;
        }

        public ConcurrentHashSet(IEnumerable<T> collection)
        {
            _dictionary = new ConcurrentDictionary<T, byte>(
                collection.Select(x => new KeyValuePair<T, byte>(x, default)));
            _comparer = EqualityComparer<T>.Default;
        }

        public ConcurrentHashSet(IEqualityComparer<T>? comparer)
        {
            _dictionary = new ConcurrentDictionary<T, byte>(comparer);
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public ConcurrentHashSet(
            int concurrencyLevel,
            int capacity)
        {
            _dictionary = new ConcurrentDictionary<T, byte>(concurrencyLevel, capacity);
            _comparer = EqualityComparer<T>.Default;
        }

        public ConcurrentHashSet(
            IEnumerable<T> collection,
            IEqualityComparer<T>? comparer)
        {
            _dictionary = new ConcurrentDictionary<T, byte>(
                collection.Select(x => new KeyValuePair<T, byte>(x, default)),
                comparer);
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public ConcurrentHashSet(
            int concurrencyLevel,
            int capacity,
            IEqualityComparer<T>? comparer)
        {
            _dictionary = new ConcurrentDictionary<T, byte>(
                concurrencyLevel,
                capacity,
                comparer);
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public ConcurrentHashSet(
            int concurrencyLevel,
            IEnumerable<T> collection,
            IEqualityComparer<T>? comparer)
        {
            _dictionary = new ConcurrentDictionary<T, byte>(
                concurrencyLevel,
                collection.Select(x => new KeyValuePair<T, byte>(x, default)),
                comparer);
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }
        #endregion ctor

        private readonly ConcurrentDictionary<T, byte> _dictionary;
        private readonly IEqualityComparer<T> _comparer;

        public int Count => _dictionary.Count;

        public bool IsEmpty => _dictionary.IsEmpty;

        public IEqualityComparer<T> Comparer => _comparer;

        public bool Add(T item) => _dictionary.TryAdd(item, default);

        public bool Remove(T item) => _dictionary.TryRemove(item, out _);

        public int RemoveWhere(Predicate<T> match)
        {
            Error.ThrowIfArgumentNull(nameof(match), match);
            int numRemoved = 0;
            foreach (var item in _dictionary.Keys)
            {
                if (match(item) &&
                    _dictionary.TryRemove(item, out _))
                {
                    numRemoved += 1;
                }
            }
            return numRemoved;
        }

        public void Clear() => _dictionary.Clear();

        public bool Contains(T item) => _dictionary.ContainsKey(item);

        public IEnumerator<T> GetEnumerator() => _dictionary.Keys.GetEnumerator();

        public void CopyTo(T[] array) => CopyTo(array, 0);

        public void CopyTo(T[] array, int arrayIndex) => _dictionary.Keys.CopyTo(array, arrayIndex);

        public void ExceptWith(IEnumerable<T> other)
        {
            Error.ThrowIfArgumentNull(nameof(other), other);
            foreach (var item in other)
            {
                _dictionary.TryRemove(item, out _);
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            Error.ThrowIfArgumentNull(nameof(other), other);
            var contains = other is ICollection<T> collection ?
                (Func<T, bool>)collection.Contains :
                (Func<T, bool>)other.Contains;
            foreach (var item in _dictionary.Keys)
            {
                if (!contains(item))
                {
                    _dictionary.TryRemove(item, out _);
                }
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            Error.ThrowIfArgumentNull(nameof(other), other);
            if (other == this)
            {
                return false;
            }
            if (other is ICollection<T> otherAsCollection)
            {
                int otherCount = otherAsCollection.Count;
                if (otherCount == 0)
                {
                    return false;
                }
                int count = _dictionary.Count;
                if (count == 0)
                {
                    return otherCount > 0;
                }
                if (other is HashSet<T> otherAsSet &&
                    EqualityComparersAreEqualTo(otherAsSet))
                {
                    if (count >= otherCount)
                    {
                        return false;
                    }
                    return IsSubsetOfHashSetWithSameComparer(otherAsSet);
                }
                if (other is ConcurrentHashSet<T> otherAsConcurrentSet &&
                    EqualityComparersAreEqualTo(otherAsConcurrentSet))
                {
                    if (count >= otherCount)
                    {
                        return false;
                    }
                    return IsSubsetOfHashSetWithSameComparer(otherAsConcurrentSet);
                }
                return _dictionary.Keys.All(otherAsCollection.Contains);
            }


        }

        public bool IsProperSupersetOf(IEnumerable<T> other) => throw new NotImplementedException();

        public bool IsSubsetOf(IEnumerable<T> other) => throw new NotImplementedException();

        public bool IsSupersetOf(IEnumerable<T> other) => throw new NotImplementedException();

        public bool Overlaps(IEnumerable<T> other) => other.Any(Contains);

        public bool SetEquals(IEnumerable<T> other) => throw new NotImplementedException();

        public void SymmetricExceptWith(IEnumerable<T> other) => throw new NotImplementedException();

        public void UnionWith(IEnumerable<T> other)
        {
            Error.ThrowIfArgumentNull(nameof(other), other);
            foreach (var item in other)
            {
                _dictionary.TryAdd(item, default);
            }
        }
        internal bool EqualityComparersAreEqualTo(ConcurrentHashSet<T> other)
        {
            return _comparer.Equals(other.Comparer);
        }

        internal bool EqualityComparersAreEqualTo(HashSet<T> other)
        {
            return _comparer.Equals(other.Comparer);
        }

        internal bool IsSubsetOfHashSetWithSameComparer(HashSet<T> other)
        {
            foreach (var item in _dictionary.Keys)
            {
                if (!other.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        internal bool IsSubsetOfHashSetWithSameComparer(
            ConcurrentHashSet<T> other)
        {
            foreach (var item in _dictionary.Keys)
            {
                if (!other._dictionary.ContainsKey(item))
                {
                    return false;
                }
            }
            return true;
        }

        #region ICollection<T>
        bool ICollection<T>.IsReadOnly => false;

        void ICollection<T>.Add(T item) => Add(item);
        #endregion ICollection<T>

        #region ICollection
        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

        void ICollection.CopyTo(Array array, int index)
        {
            Error.ThrowIfArgumentNull(nameof(array), array);

            CopyTo((T[])array, index);
        }
        #endregion ICollection

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion IEnumerable
    }
}
