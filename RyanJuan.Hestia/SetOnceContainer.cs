namespace RyanJuan.Hestia;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="throwOnRepeatSet"></param>
public class SetOnceContainer<T>(bool throwOnRepeatSet = false)
{
    private readonly object _lock = new();
    private volatile bool _isSet = false;
    private T? _value;

    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public T? Value => _isSet ? _value : throw new InvalidOperationException("The value has not been set.");

    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public bool IsSet => _isSet;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="InvalidOperationException"></exception>
    [PublicAPI]
    public void SetValue(T? value)
    {
        if (!_isSet)
        {
            lock (_lock)
            {
                if (!_isSet)
                {
                    _isSet = true;
                    _value = value;
                    return;
                }
            }
        }

        if (throwOnRepeatSet)
        {
            throw new InvalidOperationException("The value is already been set.");
        }
    }
}
