using Nexar.Client.Login;
using System;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 2)
                throw new InvalidOperationException("Usage: clientId clientSecret");

            var clientId = args[0];
            var clientSecret = args[1];

            var result = await LoginHelper.LoginAsync(clientId, clientSecret);
            var username = result.Username;
            var token = result.AccessToken;

            Console.WriteLine(username);
            Console.WriteLine(token);
        }
    }
}
