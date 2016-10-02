using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http; 
namespace Client
{
    public class Program
    {
        //sækja token
        //leiðbeiningar um það á github
        //https://github.com/IdentityServer/IdentityServer4.Samples/blob/7d768460a2eb6a704a1933ad1f99ca5f384ce8fa/Quickstarts/1_ClientCredentials/src/Client/Program.cs
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //notast við authentication serverinn
             var temp = await DiscoveryClient.GetAsync("http://localhost:5000");
             

            // request token
            var tokenClient = new TokenClient(temp.TokenEndpoint, "ro.client", "secret");  
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");
            var tokenResponse1 = await tokenClient.RequestResourceOwnerPasswordAsync("bob", "password", "api1");
            var tokenResponseAnonymous = await tokenClient.RequestResourceOwnerPasswordAsync("anon","passwod","api1");

            if (tokenResponse.IsError)  
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine("well fuck me sideways");
            Console.WriteLine(tokenResponse.AccessToken);
            var cli = new HttpClient();
            cli.SetBearerToken(tokenResponse.AccessToken);

            	//how to test post?

                //Need to fix forbidden claim
                
            var cli2 = new HttpClient();
            cli2.SetBearerToken(tokenResponse1.AccessToken);

            var cli3 = new HttpClient();
            cli3.SetBearerToken(tokenResponseAnonymous.AccessToken);
            //Test if the user has the authorization
            var response = await cli.GetAsync("http://localhost:5001/api/courses/1");
            Console.WriteLine("THIS IS THE RESPONSE*************");
            Console.WriteLine(response);
            

            var response2 = await cli2.GetAsync("http://localhost:5001/api/courses/1");
            Console.WriteLine("THIS IS THE RESPONSE*************222222222");
            Console.WriteLine(response2);
            
            var response3 = await cli3.GetAsync("http://localhost:5001/api/courses/1");
            Console.WriteLine("THIS IS THE RESPONSE*************333333333333");
            Console.WriteLine(response3);
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }

           
        }
           

        
    }
}
