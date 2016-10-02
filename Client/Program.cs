using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http; 
namespace Client
{
    public class Program
    {

        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //notast við authentication serverinn
             var temp = await DiscoveryClient.GetAsync("http://localhost:5000");
             

            // request token
            var tokenClient = new TokenClient(temp.TokenEndpoint, "ro.client", "secret");  
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");
            var tokenResponse1 = await tokenClient.RequestResourceOwnerPasswordAsync("bob", "password", "api1");
            

            if (tokenResponse.IsError)  
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine("well fuck me sideways");
            Console.WriteLine(tokenResponse.AccessToken);
            var teacher = new HttpClient();
            teacher.SetBearerToken(tokenResponse.AccessToken);

                //Need to fix forbidden claim

            var student = new HttpClient();
            student.SetBearerToken(tokenResponse1.AccessToken);

            var anon = new HttpClient();

            Console.WriteLine("***********Teacher tests***********");
            var response = await teacher.GetAsync("http://localhost:5001/api/courses/1");
            Console.WriteLine("Teacher");
            Console.WriteLine(response);
            

            var responsevol4 = await teacher.PostAsync("http://localhost:5001/api/courses/",null);
            Console.WriteLine("THIS IS THE RESPONSE*4444444444444444");
            var content = responsevol4.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            Console.WriteLine(responsevol4);
           
            Console.WriteLine("***********Student tests***********");

            var response2 = await student.GetAsync("http://localhost:5001/api/courses/1");
            Console.WriteLine("THIS IS THE RESPONSE*************222222222");
            Console.WriteLine(response2);
            
            Console.WriteLine("***********Anon tests***********");

            var response3 = await anon.GetAsync("http://localhost:5001/api/courses/1");
            Console.WriteLine("THIS IS THE RESPONSE*************333333333333");
            Console.WriteLine(response3);
            
            var response4 = await anon.PostAsync("http://localhost:5001/api/courses/",null);
            Console.WriteLine(response4);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }

           
        }
           

        
    }
}
