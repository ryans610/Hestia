namespace RyanJuan.Hestia.AspNetCore.Geo;

/// <summary>
/// Interface for geocoding latitude and longitude to the address.
/// </summary>
public interface IGeocoding
{
    /// <summary>
    /// Geocoding the specific latitude and longitude to the address.
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lng"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<string?> GetAddressFromLatLngAsync(double lat, double lng, CancellationToken cancellationToken = default);
}
