namespace Dinner.Infrastructure.Authentication
{
#pragma warning disable
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string Secret { get; init; }
        public int ExpiryMinutes { get; init; }
        public string Audience { get; init; }
        public string Issuer { get; init; }
    }
}