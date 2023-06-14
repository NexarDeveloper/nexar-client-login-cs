using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nexar.Client.Login
{
    public static class LoginHelper
    {
        private const string Authority = "https://identity.nexar.com";
        private const string RedirectUrl = "http://localhost:3000/login";

        public static async Task<LoginInfo> LoginAsync(
            string clientId,
            string clientSecret,
            string[] scopes,
            string authority = Authority)
        {
            if (clientId == null)
                throw new ArgumentNullException(nameof(clientId));
            if (clientSecret == null)
                throw new ArgumentNullException(nameof(clientSecret));
            if (authority == null)
                throw new ArgumentNullException(nameof(authority));

            var allScopes = new List<string> { "openid", "profile", "email" };
            if (scopes != null)
                allScopes.AddRange(scopes);

            var options = new OidcClientOptions
            {
                Authority = authority,
                ClientId = clientId,
                ClientSecret = clientSecret,
                RedirectUri = RedirectUrl,
                Scope = string.Join(" ", allScopes),
                FilterClaims = false,
            };

            var http = new HttpListener();
            http.Prefixes.Add(RedirectUrl.EndsWith("/") ? RedirectUrl : RedirectUrl + '/');
            http.Start();
            try
            {
                var client = new OidcClient(options);
                var state = await client.PrepareLoginAsync();

                OpenBrowser(state.StartUrl);

                // wait for the authorization response
                var context = await http.GetContextAsync();
                var data = GetRequestPostData(context.Request);

                // sends HTTP response to the browser
                var response = context.Response;
                var buffer = Encoding.UTF8.GetBytes(Html);
                response.ContentLength64 = buffer.Length;
                var responseOutput = response.OutputStream;
                await responseOutput.WriteAsync(buffer, 0, buffer.Length);
                responseOutput.Close();

                var result = await client.ProcessResponseAsync(data, state);
                return new LoginInfo(result);
            }
            finally
            {
                http.Stop();
            }
        }

        //! The oiginal sample reads the body.
        private static string GetRequestPostData(HttpListenerRequest request)
        {
            return request.Url.Query;
        }

        private static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        const string Html = @"
<html>
<head>
  <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css'>
  <title>Welcome to Nexar</title>
  <style>
    html {
      height: 100%;
      background-image: linear-gradient(to right, #000b24, #001440);
    }
    body {
      color: #ffffff;
    }
    .center {
      width: 100%;
      position: absolute;
      left: 50%;
      top: 50%;
      transform: translate(-50%, -50%);
      text-align: center;
    }
    .title {
      font-family: Montserrat, sans-serif;
      font-weight: 400;
    }
    .normal {
      font-family: Montserrat, sans-serif;
      font-weight: 300;
    }
  </style>
</head>
<body>
  <div class='center'>
    <h1 class='title'>Welcome to Nexar</h1>
    <p class='normal'>You can now return to the application.</p>
  </div>
  <script>
    setTimeout(function() { window.close() }, 1000)
  </script>
</body>
</html>
";
    }
}
