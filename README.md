# Nexar.Client.Login

Login helper for Nexar desktop clients.

[nuget.org/packages/Nexar.Client.Login](https://www.nuget.org/packages/Nexar.Client.Login/)

## Synopsis

The package provides `Nexar.Client.Login.LoginHelper.LoginAsync()`.

This method starts the system browser at the Nexar identity server login page
for signing in. Then it redirects to the local host page with the message:

> You can now return to the application.

and returns the login result to the caller.

The login result includes the Nexar token for GraphQL queries.

## Example

```csharp
using Nexar.Client.Login;

var result = await LoginHelper.LoginAsync("https://identity.nexar.com/");
string token = result.AccessToken;
```

## See also

- [Sample/Program.cs](Sample/Program.cs) - simple demo console app
- [nexar-token-cs](https://github.com/NexarDeveloper/nexar-token-cs) - console app for getting various Nexar tokens
