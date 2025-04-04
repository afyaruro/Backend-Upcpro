

namespace Domain.Base.Jwt
{
    public class JwtEntity : IJwtEntity
    {
        public string key { get; set; }

        public string issuer { get; set; }

        public string audience { get; set; }

        public int expiryMinutes { get; set; }
    }
}