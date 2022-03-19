using AspNet.OAuth.Providers.Interfaces;
using AspNet.OAuth.Providers.Models;
using AspNet.OAuth.Providers.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using static AspNet.OAuth.Providers.Constants.Constants;

namespace AspNet.OAuth.Providers.Services
{
    public class SocialUserService : ISocialUserService
    {
        private readonly IHttpService _httpService;
        private readonly OAuthOptionsDictionary _oAuthOptionsDictionary;

        public SocialUserService(
            IHttpService httpService,
            OAuthOptionsDictionary oAuthOptionsDictionary)
        {
            _httpService = httpService;
            _oAuthOptionsDictionary = oAuthOptionsDictionary;
        }

        public SocialUserModel SocialUserModel { get; private set; } = new SocialUserModel();

        async Task ISocialUserService.AuthorizeAsync(string provider, string code, string returnUrl)
        {
            switch (provider)
            {
                case Provider.GOOGLE:
                    await AuthorizeGoogleAsync(code, returnUrl);
                    break;
                case Provider.FACEBOOK:
                    await AuthorizeFacebookAsync(code, returnUrl);
                    break;
                default:
                    throw new ArgumentException(string.Format(Errors.PROVIDER_IS_NOT_SUPPORTED, provider));
            }
        }

        private async Task AuthorizeGoogleAsync(string code, string returnUrl)
        {
            var googleOpts = _oAuthOptionsDictionary[Provider.GOOGLE];

            var tokenResponse = await _httpService.PostAsync<TokenResponse>(OAuthEndpoints.GOOGLE_TOKEN_ENDPOINT, null,
                new Dictionary<string, string>()
                {
                    { Parameters.ClientId, googleOpts.ClientId },
                    { Parameters.ClientSecret, googleOpts.ClientSecret },
                    { Parameters.GrantType, "authorization_code" },
                    { Parameters.RedirectUri, returnUrl},
                    { Parameters.Code, code }
                }, null);

            var userResponse = await _httpService.PostAsync<SocialUserModel>(OAuthEndpoints.GOOGLE_USER_ENDPOINT, null, null,
                new Dictionary<string, string>()
                {
                    { Parameters.Authorization, $"{Parameters.Bearer} {tokenResponse.AccessToken}" },
                });

            SocialUserModel.Id = userResponse.Id;
            SocialUserModel.Name = userResponse.Name;
            SocialUserModel.Email = userResponse.Email;
        }

        private async Task AuthorizeFacebookAsync(string code, string returnUrl)
        {
            var facebookOpts = _oAuthOptionsDictionary[Provider.FACEBOOK];

            var tokenResponse = await _httpService.PostAsync<TokenResponse>(OAuthEndpoints.FACEBOOK_TOKEN_ENDPOINT,
                new Dictionary<string, string>()
                {
                    { Parameters.ClientId, facebookOpts.ClientId },
                    { Parameters.ClientSecret, facebookOpts.ClientSecret },
                    { Parameters.RedirectUri, returnUrl },
                    { Parameters.Code, code }
                }, null, null);

            var userResponse = await _httpService.PostAsync<SocialUserModel>(OAuthEndpoints.FACEBOOK_USER_ENDPOINT,
                new Dictionary<string, string>()
                {
                    { Parameters.Fields, "email,name" }
                },
                null,
                new Dictionary<string, string>()
                {
                    { Parameters.Authorization, $"{Parameters.Bearer} {tokenResponse.AccessToken}" },
                });

            SocialUserModel.Id = userResponse.Id;
            SocialUserModel.Name = userResponse.Name;
            SocialUserModel.Email = userResponse.Email;
        }
    }
}
