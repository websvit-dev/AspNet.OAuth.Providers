using AspNet.OAuth.Providers.Options;
using Microsoft.Extensions.DependencyInjection;

namespace AspNet.OAuth.Providers.Builders
{
    public class AuthenticationBuilder
    {
        internal OAuthOptionsDictionary Secrets { get; set; } = new OAuthOptionsDictionary();

        private IServiceCollection _serviceCollection;

        public AuthenticationBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void Build()
        {
            _serviceCollection.AddSingleton(Secrets);
        }
    }
}
