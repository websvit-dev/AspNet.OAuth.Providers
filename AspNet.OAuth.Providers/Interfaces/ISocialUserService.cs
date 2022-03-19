using AspNet.OAuth.Providers.Models;
using System.Threading.Tasks;

namespace AspNet.OAuth.Providers.Interfaces
{
    public interface ISocialUserService
    {
        SocialUserModel SocialUserModel { get; }

        internal Task AuthorizeAsync(string provider, string code, string returnUrl);
    }
}
