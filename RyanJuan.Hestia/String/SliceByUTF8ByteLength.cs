namespace RyanJuan.Hestia;

public static partial class HestiaString
{
#if ZH_HANT
#else
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="byteLength"></param>
    /// <returns></returns>
#endif
    public static string SliceByUTF8ByteLength(
        this string value,
        int byteLength)
    {
        Error.ThrowIfArgumentNull(nameof(value), value);
        if (byteLength < 0)
        {
            throw Error.ArgumentOutOfRange(
                nameof(byteLength),
                $"{nameof(byteLength)} is less than 0.",
                byteLength);
        }
        if (byteLength == 0)
        {
            return string.Empty;
        }
        var bytes = Encoding.UTF8.GetBytes(value);
        if (bytes.Length <= byteLength)
        {
            return value;
        }
        int edgeIndex = byteLength;
        if ((bytes[edgeIndex] & 0b_1000_0000) != 0b_0000_0000)
        {
            while (edgeIndex > 0 &&
                   (bytes[edgeIndex] & 0b_1100_0000) != 0b_1100_0000)
            {
                edgeIndex -= 1;
            }
        }
        if (edgeIndex == 0)
        {
            return string.Empty;
        }
        return Encoding.UTF8.GetString(bytes, 0, edgeIndex);
    }
}
