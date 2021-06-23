using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http.Formatting;

namespace Movie
{
    class Program
    {
        static void Main(string[] args)
        {
            Search("flubber");
            Console.WriteLine("Hello World!");
        }

        public class DataObject
        {
            public string Result { get; set; }
        }

        static void Search(string title)
        {
            const string URL = "https://www.omdbapi.com/";
            const string apiKey = "&apikey=23bb5b7a";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            string searchParams = "?" + title + apiKey;

            // List data response.
            HttpResponseMessage response = client.GetAsync(searchParams).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content);
                // Parse the response body..
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {
                    Console.WriteLine("{0}", d);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            return;
        }
    }
}
