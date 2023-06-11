using Nexar.Client.Login;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 2)
                throw new InvalidOperationException("Usage: clientId clientSecret [scope1 ...]");

            var clientId = args[0];
            var clientSecret = args[1];
            var scopes = args.Skip(2).ToArray();

            var result = await LoginHelper.LoginAsync(clientId, clientSecret, scopes);
            var token = result.AccessToken;
            var username = result.Username;

            Console.WriteLine(token);
            Console.WriteLine(username);
        }
    }
}
