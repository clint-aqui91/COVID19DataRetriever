using COVID19DataRetriever.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text.Json;

namespace COVID19DataRetriever.Controllers
{
    public class COVID19Statistics : Controller
    {
        private Task<CovidDataModel> task;
        private string body;

        public class Covid19StatisticsRoot
        {
            public List<Covid19Statistics>? data { get; set; }
        }

        public class Covid19Statistics
        {
            public string date { get; set; }
            public int confirmed { get; set; }
            public int deaths { get; set; }
            public int recovered { get; set; }
            public int confirmed_diff { get; set; }
            public int deaths_diff { get; set; }
            public int recovered_diff { get; set; }
            public string last_update { get; set; }
            public int active { get; set; }
            public int active_diff { get; set; }
            public double fatality_rate { get; set; }
            public Region region { get; set; }
        }

        public class Region
        {
            public string iso { get; set; }
            public string name { get; set; }
            public string province { get; set; }
            public string lat { get; set; }
            public string @long { get; set; }
            public List<object> cities { get; set; }
        }

        


        public async Task<IActionResult> StatisticsAsync()
        {
            CovidDataModel covidDataModelObject = new CovidDataModel();
            var serializedObject = await GetSerializedDataFromAPIAsync();
            Console.WriteLine(serializedObject);

            // Deserialize JSON Object retrieved from API and C# classes converted from JSON using https://json2csharp.com/
            // JSON Deserialization library source: Newtonsoft.Json, reference: https://www.newtonsoft.com/json & https://www.nuget.org/packages/Newtonsoft.Json/
            // An object (based on the API's JSON object root is created, containing a list of COVID19Statistics with each containing a list of Region data.
            var obj = JsonConvert.DeserializeObject<Covid19StatisticsRoot>(serializedObject);
            covidDataModelObject.ActiveCases = obj.data[0].active;

            Console.WriteLine("Active Cases = " + covidDataModelObject.ActiveCases);
            return View(covidDataModelObject);

        }

		public async Task<String> GetSerializedDataFromAPIAsync()
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports?q=Malta&region_name=Malta&iso=MLT"),
				Headers =
				{
					{ "X-RapidAPI-Key", "e286b44a29msh4c7a6448753438ap1f11a5jsn85743f38880b" },
					{ "X-RapidAPI-Host", "covid-19-statistics.p.rapidapi.com" },
				},
			};
			
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				Console.WriteLine(body);
				return body;
			}
			//return body;
		}


        /*
         * CODE from RAPID API
         * var client = new HttpClient();
var request = new HttpRequestMessage
{
	Method = HttpMethod.Get,
	RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports?q=Malta&region_name=Malta&iso=MLT"),
	Headers =
	{
		{ "X-RapidAPI-Key", "e286b44a29msh4c7a6448753438ap1f11a5jsn85743f38880b" },
		{ "X-RapidAPI-Host", "covid-19-statistics.p.rapidapi.com" },
	},
};
using (var response = await client.SendAsync(request))
{
	response.EnsureSuccessStatusCode();
	var body = await response.Content.ReadAsStringAsync();
	Console.WriteLine(body);
}
         */
    }
}
