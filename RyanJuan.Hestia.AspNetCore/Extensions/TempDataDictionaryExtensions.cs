using Microsoft.AspNetCore.Mvc.ViewFeatures;

using System.Text.Json;

using JetBrains.Annotations;

namespace RyanJuan.Hestia.AspNetCore.Extensions;

public static class TempDataDictionaryExtensions
{
    [PublicAPI]
    public static void Set<T>(this ITempDataDictionary tempData, string key, T value)
    {
        ArgumentNullException.ThrowIfNull(tempData);
        ArgumentNullException.ThrowIfNull(key);
        tempData[key] = JsonSerializer.Serialize(value);
    }

    [PublicAPI]
    public static T? Get<T>(this ITempDataDictionary tempData, string key)
    {
        ArgumentNullException.ThrowIfNull(tempData);
        ArgumentNullException.ThrowIfNull(key);
        if (tempData.TryGetValue(key, out var value))
        {
            return DeserializeValueOrDefault<T>(value);
        }

        return default;
    }

    [PublicAPI]
    public static T? Peek<T>(this ITempDataDictionary tempData, string key)
    {
        ArgumentNullException.ThrowIfNull(tempData);
        ArgumentNullException.ThrowIfNull(key);
        var value = tempData.Peek(key);
        return DeserializeValueOrDefault<T>(value);
    }

    private static T? DeserializeValueOrDefault<T>(object? value)
    {
        if (value is string stringValue)
        {
            return JsonSerializer.Deserialize<T>(stringValue);
        }

        return default;
    }
}
