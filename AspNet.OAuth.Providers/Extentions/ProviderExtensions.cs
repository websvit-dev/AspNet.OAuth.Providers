using AspNet.OAuth.Providers.Builders;
using AspNet.OAuth.Providers.Options;
using System;
using static AspNet.OAuth.Providers.Constants.Constants;

namespace AspNet.OAuth.Providers.Extentions
{
    public static class ProviderExtensions
    {
        public static AuthenticationBuilder AddGoogle(this AuthenticationBuilder builder, Action<OAuthOptions> authOptionsAction)
        {
            builder.Secrets.Add(Provider.GOOGLE, GetOption(authOptionsAction));
            return builder;
        }

        public static AuthenticationBuilder AddFacebook(this AuthenticationBuilder builder, Action<OAuthOptions> authOptionsAction)
        {
            builder.Secrets.Add(Provider.FACEBOOK, GetOption(authOptionsAction));
            return builder;
        }

        private static OAuthOptions GetOption(Action<OAuthOptions> authOptionsAction)
        {
            var authOption = new OAuthOptions();
            authOptionsAction.Invoke(authOption);

            return authOption;
        }
    }
}
