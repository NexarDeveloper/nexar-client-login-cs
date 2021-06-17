using Nexar.Client.Login;
using System;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static async Task Main()
        {
            var result = await LoginHelper.LoginAsync("https://identity.nexar.com");
            var username = result.Username;
            var token = result.AccessToken;

            Console.WriteLine(username);
            Console.WriteLine(token);
        }
    }
}
