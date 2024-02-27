using System.Collections;

namespace RyanJuan.Hestia.Resources;

internal class HashtableAsSetAdapter : ICollection, IEnumerable
{
    public HashtableAsSetAdapter()
        : this(null!, null)
    { }

    public HashtableAsSetAdapter(
        IEqualityComparer? comparer)
        : this(null!, comparer)
    { }

    public HashtableAsSetAdapter(
        IEnumerable data)
        : this(data, null)
    { }

    public HashtableAsSetAdapter(
        IEnumerable data,
        IEqualityComparer? comparer)
    {
        _hashtable = new Hashtable(comparer);
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (data is not null)
        {
            foreach (var value in data)
            {
                if (value is null)
                {
                    _hasNull = true;
                }
                else
                {
                    Add(value);
                }
            }
        }
    }

    private readonly Hashtable _hashtable;
    private volatile bool _hasNull = false;

    public int Count => _hasNull ? _hashtable.Count + 1 : _hashtable.Count;

    public IEnumerator GetEnumerator()
    {
        foreach (var key in _hashtable.Keys)
        {
            yield return key;
        }
        if (_hasNull)
        {
            yield return null;
        }
    }

    public bool Contains(object? value)
    {
        return value is null ? _hasNull : _hashtable.ContainsKey(value);
    }

    public bool Add(object? value)
    {
        if (value is null)
        {
            if (_hasNull)
            {
                return false;
            }
            _hasNull = true;
            return true;
        }

        if (_hashtable.ContainsKey(value))
        {
            return false;
        }
        _hashtable.Add(value, null);
        return true;
    }

    public void Remove(object? value)
    {
        if (value is null)
        {
            if (_hasNull)
            {
                _hasNull = false;
            }
        }
        else
        {
            _hashtable.Remove(value);
        }
    }

    public void Clear()
    {
        _hasNull = false;
        _hashtable.Clear();
    }

    public void CopyTo(Array array, int index)
    {
        _hashtable.CopyTo(array, index);
    }

    object ICollection.SyncRoot => _hashtable.SyncRoot;
    bool ICollection.IsSynchronized => false;
}
