namespace carcompanion.Security
{
    public class JwtSettings
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string TokenLifeTime { get; set; }
        public string RefreshTokenLifeTime { get; set; }
        
    }
}