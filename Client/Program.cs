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

            //Teacher
            var teacher = new HttpClient();
            teacher.SetBearerToken(tokenResponse.AccessToken);
 
            //Student
            var student = new HttpClient();
            student.SetBearerToken(tokenResponse1.AccessToken);

            //Anonymous
            var anon = new HttpClient();

            Console.WriteLine("***********Teacher tests***********");

            //Get on all courses
            var response = await teacher.GetAsync("http://localhost:5001/api/courses");
            Console.WriteLine("Get on all courses");
            Console.WriteLine("Response: " + response.ReasonPhrase);

            //Get on single course
            var response2 = await teacher.GetAsync("http://localhost:5001/api/courses/1");
            Console.WriteLine("Get on a single course");
            Console.WriteLine("Response: " + response2.ReasonPhrase);

            //post a course
            var response3 = await teacher.PostAsync("http://localhost:5001/api/courses/",null);
            Console.WriteLine("Post on a single course");
            Console.WriteLine("Response: " + response3.ReasonPhrase);
           
            Console.WriteLine("");

            Console.WriteLine("***********Student tests***********");

            //Get on all courses
            var studentresp1 = await student.GetAsync("http://localhost:5001/api/courses");
            Console.WriteLine("Get on all courses");
            Console.WriteLine("Response: " + studentresp1.ReasonPhrase);

            //Get on a single course            
            var studentresp2 = await student.GetAsync("http://localhost:5001/api/courses/1");
            Console.WriteLine("Get on a single course");
            Console.WriteLine("Response: " + studentresp2.ReasonPhrase);

            //post a course
            var studentresp3 = await student.PostAsync("http://localhost:5001/api/courses/",null);
            Console.WriteLine("Post on a single course");
            Console.WriteLine("Response: " + studentresp3.ReasonPhrase);
            
            Console.WriteLine("");

            Console.WriteLine("***********Anonymous tests***********");

            //Get on all courses
            var anonresp1 = await anon.GetAsync("http://localhost:5001/api/courses");
            Console.WriteLine("Get on all courses");
            Console.WriteLine("Response: " + anonresp1.ReasonPhrase);

            //Get on a single course            
            var anonresp2 = await anon.GetAsync("http://localhost:5001/api/courses/1");
            Console.WriteLine("Get on a single course");
            Console.WriteLine("Response: " + anonresp2.ReasonPhrase);

            //post a course
            var anonresp3 = await anon.PostAsync("http://localhost:5001/api/courses/",null);
            Console.WriteLine("Post on a single course");
            Console.WriteLine("Response: " + anonresp3.ReasonPhrase);

            Console.WriteLine("");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }

           
        }
           

        
    }
}
