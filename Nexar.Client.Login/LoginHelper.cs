using IdentityModel.OidcClient;
using System;
using System.Threading.Tasks;

namespace Nexar.Client.Login
{
    public static class LoginHelper
    {
        static OidcClient _oidcClient;

        public static async Task<LoginInfo> LoginAsync(
            string clientId,
            string clientSecret,
            string authority = "https://identity.nexar.com/")
        {
            if (clientId == null)
                throw new ArgumentNullException(nameof(clientId));
            if (clientSecret == null)
                throw new ArgumentNullException(nameof(clientSecret));
            if (authority == null)
                throw new ArgumentNullException(nameof(authority));

            var browser = new SystemBrowser(3000);

            var options = new OidcClientOptions
            {
                Authority = authority,
                ClientId = clientId,
                ClientSecret = clientSecret,
                RedirectUri = "http://localhost:3000/login",
                // Adjust the below scopes (specifically design.domain, supply.domain) based on your applications permissions
                Scope = "openid profile email user.access design.domain supply.domain",
                FilterClaims = false,
                Browser = browser,
                Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect
            };

            _oidcClient = new OidcClient(options);
            var result = await _oidcClient.LoginAsync(new LoginRequest());
            return new LoginInfo(result);
        }
    }
}
