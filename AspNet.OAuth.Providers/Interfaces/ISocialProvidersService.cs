

namespace AspNet.OAuth.Providers.Interfaces
{
    public interface ISocialProvidersService
    {
        string GetRefrence(string provider, string returnUrl);
    }
}
