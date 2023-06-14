# Nexar.Client.Login

Login helper for Nexar desktop clients.

[nuget.org/packages/Nexar.Client.Login](https://www.nuget.org/packages/Nexar.Client.Login/)

## Synopsis

The package provides the only method: `Nexar.Client.Login.LoginHelper.LoginAsync()`.

This method opens the system browser at the Nexar login page for signing in and
returns the login result to the caller.  The result includes the access token
for Nexar services and, with `user.access`, the user name.

## Example

```csharp
using Nexar.Client.Login;

var scopes = new string[] { "user.access", "design.domain", "supply.domain" };
var result = await LoginHelper.LoginAsync(clientId, clientSecret, scopes);

var token = result.AccessToken;
var username = result.Username;
```

## Login / Logout

The default system browser opens the Nexar identity login page.
To login, enter your credentials and click `Sign In`.

The browser does not necessarily asks your credentials each time, it may remember and use your last login.
If this is not desired, open <https://identity.nexar.com> and either logout or clean the site cookies.

## See also

- [Program.cs](https://github.com/NexarDeveloper/nexar-client-login-cs/blob/main/Sample/Program.cs) - demo console app
- [nexar-token-cs](https://github.com/NexarDeveloper/nexar-token-cs) - console app for getting various Nexar tokens
- [nexar-console-design](https://github.com/NexarDeveloper/nexar-templates/blob/main/nexar-console-design) - getting a token and using for operations
