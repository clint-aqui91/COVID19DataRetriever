using COVID19DataRetriever.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COVID19DataRetriever.Controllers
{
    public class COVID19Statistics : Controller
    {
        private Task<CovidDataModel> task;
        private string body;

        public async Task<IActionResult> StatisticsAsync()
        {
            CovidDataModel covidDataModelObject = new CovidDataModel();
            var serializedObject = await GetSerializedDataFromAPIAsync();
            Console.WriteLine(serializedObject);
			//var deserializedObject = JsonSerializer.Deserialize<JsonDeserializedObjectModel>(serializedObject);
            JsonDeserializedObjectModel? jsonDeserializedObjectModel = JsonSerializer.Deserialize<JsonDeserializedObjectModel>(serializedObject);
            Console.WriteLine(jsonDeserializedObjectModel?.confirmed);

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
