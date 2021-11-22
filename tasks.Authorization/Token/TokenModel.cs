using System;

namespace Tasks.Authorization.Token
{
    public class TokenModel
    {
        public SecurityAccessToken SecurityToken { get; set; }
        public RefreshAccessToken RefreshToken { get; set; }
    }

    public class SecurityAccessToken
    {
        public string JwtType { get; set; }
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }

    public class RefreshAccessToken
    {
        public string JwtType { get; set; }
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
