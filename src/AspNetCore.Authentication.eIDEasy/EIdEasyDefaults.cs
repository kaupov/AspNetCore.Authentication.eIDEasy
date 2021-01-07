namespace AspNetCore.Authentication.eIDEasy
{
    public class EIdEasyDefaults
    {
        public const string AuthenticationScheme = "eIDEasy";

        public const string DisplayName = "eIDEasy";

        public const string AuthorizationEndpoint = "https://id.eideasy.com/oauth/authorize";

        public const string TokenEndpoint = "https://id.eideasy.com/oauth/access_token";

        public const string UserInformationEndpoint = "https://id.eideasy.com/api/v2/user_data";
    }
}