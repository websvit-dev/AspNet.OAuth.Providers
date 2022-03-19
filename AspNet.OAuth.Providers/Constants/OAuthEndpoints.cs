

namespace AspNet.OAuth.Providers.Constants
{
    public partial class Constants
    {
        public static class OAuthEndpoints
        {
            public const string GOOGLE_AUTH_ENDPOINT = "https://accounts.google.com/o/oauth2/v2/auth" +
                "?redirect_uri={0}" +
                "&client_id={1}" +
                "&response_type=code" +
                "&state=Google" +
                "&scope=https://www.googleapis.com/auth/userinfo.profile%20https://www.googleapis.com/auth/userinfo.email";

            public const string GOOGLE_TOKEN_ENDPOINT = "https://www.googleapis.com/oauth2/v4/token";
            public const string GOOGLE_USER_ENDPOINT = "https://www.googleapis.com/oauth2/v3/userinfo";

            public const string FACEBOOK_AUTH_ENDPOINT = "https://www.facebook.com/dialog/oauth" +
                "?redirect_uri={0}" +
                "&client_id={1}" +
                "&response_type=code" +
                "&state=Facebook" +
                "&scope=public_profile" +
                "&display=popup";

            public const string FACEBOOK_TOKEN_ENDPOINT = "https://graph.facebook.com/v3.1/oauth/access_token";
            public const string FACEBOOK_USER_ENDPOINT = "https://graph.facebook.com/v3.1/me";
        }
    }
}
