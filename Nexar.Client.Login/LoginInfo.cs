using IdentityModel.OidcClient;
using System.Linq;

namespace Nexar.Client.Login
{
    public sealed class LoginInfo
    {
        LoginResult Tag { get; }

        public string AccessToken =>
            Tag.AccessToken;

        public string Username =>
            Tag.User.Claims.FirstOrDefault(x => x.Type == "username")?.Value;

        internal LoginInfo(LoginResult tag)
        {
            Tag = tag;
        }
    }
}
