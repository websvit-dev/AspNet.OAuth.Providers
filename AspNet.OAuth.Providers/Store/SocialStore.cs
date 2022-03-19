using static AspNet.OAuth.Providers.Constants.Constants;

namespace AspNet.OAuth.Providers.Store
{
    internal static class SocialStore
    {
        internal static string CallbackEndpoint { get; set; } = Defaults.CALLBACK_ENDPOINT;
    }
}
