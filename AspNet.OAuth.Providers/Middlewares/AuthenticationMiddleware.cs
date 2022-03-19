using AspNet.OAuth.Providers.Interfaces;
using AspNet.OAuth.Providers.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using static AspNet.OAuth.Providers.Constants.Constants;

namespace AspNet.OAuth.Providers.Middlewares
{
    public class AuthenticationMiddleware
    {
        private RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ISocialUserService socialUserService)
        {
            var qs = context.Request.QueryString;
            var path = context.Request.Path;

            if (path == $"/{Defaults.SOCIAL_AUTHENTICATION_ENDPOINT}")
            {
                string provider = context.Request.Query[Parameters.Provider];
                string code = context.Request.Query[Parameters.Code];

                // code clearing, should be improved
                code = code.Replace("%2F", "/");

                StreamReader reader = new StreamReader(context.Request.Body);
                var requestBodyModel = JsonConvert.DeserializeObject<RequestBodyModel>(await reader.ReadToEndAsync());
                socialUserService.SocialUserModel.Payload = requestBodyModel.Payload;

                await socialUserService.AuthorizeAsync(provider, code, requestBodyModel.ReturnUrl);
            }

            await _next.Invoke(context);
        }
    }
}
