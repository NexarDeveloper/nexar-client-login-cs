using IdentityModel.OidcClient;
using System.Threading.Tasks;

namespace Nexar.Client.Login
{
    public static class LoginHelper
    {
        static OidcClient _oidcClient;

        public static async Task<LoginInfo> LoginAsync(string authority)
        {
            var browser = new SystemBrowser(3000);

            var options = new OidcClientOptions
            {
                Authority = authority,
                ClientId = "mvc",
                ClientSecret = "secret",
                RedirectUri = "http://localhost:3000/login",
                Scope = "openid profile email phone address a365.profile",
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
