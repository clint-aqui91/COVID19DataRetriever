using COVID19DataRetriever.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Principal;
using System.Text.Json;

namespace COVID19DataRetriever.Controllers
{
    /// <summary>
    /// Class <c>COVID19Statistics</c> is the controller class containing the controller action method which holds the logic to retrieve the data from the API, populates it into the data model and returns the view with
    /// the data model object.
    /// </summary>
    public class COVID19Statistics : Controller
    {

        // 3 classes to match the JSON object returned from the API. Classes are generated using the https://json2csharp.com/ online tool, from the string (serialized JSON) returned from the API.
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



        /// <summary>
        /// Method <c>StatisticsAsync</c> is the controller action method responsible of performing the necessary functions to retrieve the data (serialized JSON) from the API, populates it into the data model (from
        /// deserialized JSON format) and returns the view with the data model object. The Data retrieval from the API is achieved by another method, GetSerializedDataFromAPIAsync (to follow the Single Responsibility
        /// Principle - each method has a single responsibility).
        /// </summary>
        public async Task<IActionResult> StatisticsAsync()
        {
            CovidDataModel covidDataModelObject = new CovidDataModel();
            string? serializedJSONObject = await GetSerializedDataFromAPIAsync();

            //Console.WriteLine(serializedJSONObject);

            // If no exception was encountered during the data retrieval from the API, deserialize the JSON string to an object, populate the data model with the same object (deserializing the JSON object first).
            if (serializedJSONObject != "Data Retrieval from API Failed")
            {
                // Deserialize JSON Object retrieved from API and C# classes converted from JSON using https://json2csharp.com/
                // JSON Deserialization library source: Newtonsoft.Json, reference: https://www.newtonsoft.com/json & https://www.nuget.org/packages/Newtonsoft.Json/

                // An object (based on the API's JSON object root is created, containing a list of COVID19Statistics with each containing a list of Region data.
                Covid19StatisticsRoot objectFromDeserializedJSONObject = JsonConvert.DeserializeObject<Covid19StatisticsRoot>(serializedJSONObject);

                // Populate the data model object from the deserialized JSON object
                covidDataModelObject.ActiveCasesFromAPI = objectFromDeserializedJSONObject.data[0].active;
                covidDataModelObject.ConfirmedCases = objectFromDeserializedJSONObject.data[0].confirmed;

                // API appears to return Active cases + Confirmed Cases, thus a mathemical operation/subtraction is performed to get the current active cases
                covidDataModelObject.CalculatedActiveCases = (covidDataModelObject.ConfirmedCases - covidDataModelObject.ActiveCasesFromAPI);
                covidDataModelObject.ConfirmedDeaths = objectFromDeserializedJSONObject.data[0].deaths;


                // The Culture Info of my machine is en-MT, however, the last updated date from the API is en-US. DateTime.Parse was used to ensure that the Date is always converted to the desired/requested culure (dd/mm/yy),
                // en-GB (English Great Britian is used).
                var cultureInfo = new CultureInfo("en-GB");
                covidDataModelObject.DateLastUpdated = DateTime.Parse(objectFromDeserializedJSONObject.data[0].last_update, cultureInfo);

                // For testing & debugging purposes
                Console.WriteLine("Confirmed Cases = " + covidDataModelObject.ConfirmedCases);
                Console.WriteLine("Active Cases From API = " + covidDataModelObject.ActiveCasesFromAPI);
                Console.WriteLine("Calculated Active Cases = " + covidDataModelObject.CalculatedActiveCases);
                Console.WriteLine("Original Updated Date from API = " + covidDataModelObject.DateLastUpdated);
                Console.WriteLine("Confirmed Deaths = " + covidDataModelObject.ConfirmedDeaths);
                Console.WriteLine(CultureInfo.CurrentCulture);
            }

            // else hence an exception was encountered during the data retrieval from the, set the APIInteractionResult boolean value to false, which will be used to display an error message in the view.
            else
            {
                covidDataModelObject.APIInteractionResult = false;
            }

            // return View with the data model object to user/internet browser
            return View(covidDataModelObject);

        }

        /// <summary>
        /// Method <c>GetSerializedDataFromAPIAsync</c> is the method responsible of sending a request to the API and retrieving the returned data from the API's response. Exception handling is also used here.
        /// Code was provided from Rapid API, and amended - original code contained in the comment block after this method.
        /// </summary>
        public async Task<String> GetSerializedDataFromAPIAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports?q=Malta&region_name=Malta&iso=MLT"),
                    Headers =
                {
                    { "X-RapidAPI-Key", "e286b44a29msh4c7a6448753438ap1f11a5jsn85743f38880b" },
                    { "X-RapidAPI-Host", "covid-19-statistics.p.rapidapi.com" },
                },
                };

                using (HttpResponseMessage response = await httpClient.SendAsync(httpRequest))
                {
                    response.EnsureSuccessStatusCode();
                    string? body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                    return body;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                string? body = "Data Retrieval from API Failed";
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
