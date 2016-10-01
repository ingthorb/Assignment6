using System;

namespace Client
{
    public class Program
    {
        //request á localhost 5001
        //sækja token
        //leiðbeiningar um það á github
        //https://github.com/IdentityServer/IdentityServer4.Samples/blob/7d768460a2eb6a704a1933ad1f99ca5f384ce8fa/Quickstarts/1_ClientCredentials/src/Client/Program.cs
        public static void Main(string[] args)
        {
            var temp = await DiscoveryClient.GetAsync("http://localhost:5000");
            // request token
/*var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}*/

        }
    }
}
