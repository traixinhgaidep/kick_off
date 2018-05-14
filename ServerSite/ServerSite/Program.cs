using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServerSite
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:3773/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                Console.WriteLine("Server open " + baseAddress);

                var response = client.GetAsync(baseAddress + "api/templates").Result;
                //var response = client.GetAsync(baseAddress + "api/account").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
        }
    }
}
