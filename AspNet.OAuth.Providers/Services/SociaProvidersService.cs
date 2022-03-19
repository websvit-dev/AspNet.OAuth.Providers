using AspNet.OAuth.Providers.Interfaces;
using AspNet.OAuth.Providers.Options;
using System;
using static AspNet.OAuth.Providers.Constants.Constants;

namespace AspNet.OAuth.Providers.Services
{
    public class SociaProvidersService : ISocialProvidersService
    {
        private readonly OAuthOptionsDictionary _oAuthOptionsDictionary;

        public SociaProvidersService(OAuthOptionsDictionary oAuthOptionsDictionary)
        {
            _oAuthOptionsDictionary = oAuthOptionsDictionary;
        }

        public string GetRefrence(string provider, string returnUrl)
        {
            var oAuthOptions = _oAuthOptionsDictionary[provider];

            return string.Format(
                provider switch
                {
                    Provider.GOOGLE => OAuthEndpoints.GOOGLE_AUTH_ENDPOINT,
                    Provider.FACEBOOK => OAuthEndpoints.FACEBOOK_AUTH_ENDPOINT,
                    _ => throw new ArgumentException(string.Format(Errors.PROVIDER_IS_NOT_SUPPORTED, provider))
                },
                returnUrl,
                oAuthOptions.ClientId);
        }
    }
}
