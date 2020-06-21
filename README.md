# GoogleCloudPrint.NET-Example
Send a PDF to a cloud printer (Console Example)


# How to set up the auth 

Register an app with Google and Get an App Id and Client Secret
https://developers.google.com/cloud-print


# Setup auth

Step 1:
```csharp
var clientId = "#client-id#";
var clientSecret = "#client-secret#";
var provider = new GoogleCloudPrintOAuth2Provider(clientId, clientSecret);
var redirectUri = "http://localhost";
//// You should have your redirect uri here if your app is a server application, o.w. leaving blank is ok
var url = provider.BuildAuthorizationUrl(redirectUri);
Console.WriteLine(url);
```
Step 2:
```csharp
var authorizationCode = "#auth-code-here#";
var token = await provider.GenerateRefreshTokenAsync(authorizationCode, redirectUri);

string json = JsonConvert.SerializeObject(token);

write string to file
System.IO.File.WriteAllText(@"cerd.json", json);	
```


# Get Printer Info
Printer -> Details -> Advanced Details
Get Printer Id and Proxy

![Printer Details](https://i.imgur.com/AZAOm3a.png)

