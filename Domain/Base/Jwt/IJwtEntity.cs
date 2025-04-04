

namespace Domain.Base.Jwt
{
    public interface IJwtEntity
    {
        public string key { get; }
        public string issuer { get; }
        public string audience { get; }
        public int expiryMinutes { get; }
    }
}