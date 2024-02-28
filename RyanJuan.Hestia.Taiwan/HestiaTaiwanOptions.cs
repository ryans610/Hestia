namespace RyanJuan.Hestia.Taiwan;

public record HestiaTaiwanOptions
{
    public string StandardDateFormat { get; set; } = HestiaTaiwan.DefaultStandardDateFormat;
    public string StandardTimeFormat { get; set; } = HestiaTaiwan.DefaultStandardTimeFormat;
    public string StandardDateTimeFormat { get; set; } = HestiaTaiwan.DefaultStandardDateTimeFormat;
    public string RocDateFormat { get; set; } = HestiaTaiwan.DefaultRocDateFormat;
    public string RocTimeFormat { get; set; } = HestiaTaiwan.DefaultRocTimeFormat;
    public string RocDateTimeFormat { get; set; } = HestiaTaiwan.DefaultRocDateTimeFormat;
}
