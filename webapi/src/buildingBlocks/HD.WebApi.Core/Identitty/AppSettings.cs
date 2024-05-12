namespace HD.WebApi.Core.Identitty;

public class AppSettings
{
    public string Secret { get; set; }
    public int ExpireHour { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
