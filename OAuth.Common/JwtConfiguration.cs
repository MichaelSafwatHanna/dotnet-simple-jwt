namespace OAuth.Common
{
    public class JwtConfiguration
    {
        public string Secret { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public int ExpiredInMinutes { get; set; }
    }
}
