using AspNet.OAuth.Providers.Builders;
using AspNet.OAuth.Providers.Interfaces;
using AspNet.OAuth.Providers.Services;
using AspNet.OAuth.Providers.Store;
using Microsoft.Extensions.DependencyInjection;
using static AspNet.OAuth.Providers.Constants.Constants;

namespace AspNet.OAuth.Providers.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddSocialAuthentication(this IServiceCollection serviceCollection, string callbackEndpoint = Defaults.CALLBACK_ENDPOINT)
        {
            serviceCollection.AddScoped<ISocialProvidersService, SociaProvidersService>();
            serviceCollection.AddScoped<IHttpService, HttpService>();
            serviceCollection.AddScoped<ISocialUserService, SocialUserService>();

            SocialStore.CallbackEndpoint = callbackEndpoint;
            return new AuthenticationBuilder(serviceCollection);
        }
    }
}
