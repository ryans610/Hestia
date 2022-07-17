using System.Collections.Concurrent;

namespace RyanJuan.Hestia.AspNetCore.Geo;

public class GeocodingCache : IGeocoding
{
    /// <inheritdoc cref="GeocodingCache"/>
    /// <param name="geocoding"></param>
    public GeocodingCache(IGeocoding geocoding)
    {
        _geocoding = geocoding;
    }

    private readonly IGeocoding _geocoding;
    private readonly ConcurrentDictionary<(double lat, double lng), string> _cacheGeocoding =
        new();

    public async ValueTask<string?> GetAddressFromLatLngAsync(double lat, double lng, CancellationToken cancellationToken = default)
    {
        if (_cacheGeocoding.TryGetValue((lat, lng), out var result))
        {
            return result;
        }
        result = await _geocoding.GetAddressFromLatLngAsync(lat, lng, cancellationToken);
        result ??= string.Empty;
        Add(lat, lng, result);
        return result;
    }

    /// <summary>
    /// Add a geocoding result to the cache.
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lng"></param>
    /// <param name="address"></param>
    public void Add(double lat, double lng, string address)
    {
        _cacheGeocoding.TryAdd((lat, lng), address);
    }
}
