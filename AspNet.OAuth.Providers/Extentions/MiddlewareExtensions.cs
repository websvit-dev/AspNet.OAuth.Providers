using AspNet.OAuth.Providers.Middlewares;
using AspNet.OAuth.Providers.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Text;
using static AspNet.OAuth.Providers.Constants.Constants;

namespace AspNet.OAuth.Providers.Extentions
{
    public static class MiddlewareExtensions
    {
        public static void UseSocialAuthentication(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthenticationMiddleware>();

            var options = new RewriteOptions()
                            .AddRewrite(Defaults.SOCIAL_AUTHENTICATION_ENDPOINT, SocialStore.CallbackEndpoint, true);
            app.UseRewriter(options);
        }
    }
}
