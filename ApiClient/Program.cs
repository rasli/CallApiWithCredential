using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var t = Task.Run(() => GetURI(new Uri("http://172.16.1.161/sdk.cgi?action=get.playback.rtspurl&sid=0"))); //change with api route
                t.Wait();

                if (t.Result != string.Empty)
                {
                    Console.WriteLine(t.Result);
                }
                else
                {
                    Console.WriteLine("no result");
                }

                Console.ReadLine();


            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }


        }


        static async Task<string> GetURI(Uri uri)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {

                var byteArray = Encoding.ASCII.GetBytes("admin:master1234"); //change with appropriate username and password
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


                HttpResponseMessage result = await client.GetAsync(uri);
                HttpContent content = result.Content;

                if (result.IsSuccessStatusCode)
                {
                    response = await content.ReadAsStringAsync();
                }
            }

            return response;
        }


    }
}
