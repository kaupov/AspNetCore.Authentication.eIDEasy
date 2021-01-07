namespace AspNetCore.Authentication.eIDEasy
{
    public class EIdEasyDefaults
    {
        public const string AuthenticationScheme = "eIDEasy";

        public const string DisplayName = "eIDEasy";

        public const string AuthorizationEndpoint = "https://id.eideasy.com/oauth/authorize";

        public const string TokenEndpoint = "https://id.eideasy.com/oauth/access_token";

        public const string UserInformationEndpoint = "https://id.eideasy.com/api/v2/user_data";

        internal const string SBAuthorizationEndpoint = "https://test.eideasy.com/oauth/authorize";

        internal const string SBTokenEndpoint = "https://test.eideasy.com/oauth/access_token";

        internal const string SBUserInformationEndpoint = "https://test.eideasy.com/api/v2/user_data";

        internal const string SBClientId = "2IaeiZXbcKzlP1KvjZH9ghty2IJKM8Lg";

        internal const string SBClientSecret = "56RkLgZREDi1H0HZAvzOSAVlxu1Flx41";
    }
}