using COVID19DataRetriever.DataAccessLayer;
using COVID19DataRetriever.Models;
using Newtonsoft.Json;
using System.Globalization;
using static COVID19DataRetriever.ApplicationLogicLayer.DeserializedCOVID19Statistics;

namespace COVID19DataRetriever.ApplicationLogicLayer
{
    public class ApplicationLogic
    {
        /// <summary>
        /// Method <c>GetSerializedDataFromAPIAsync</c> is the method responsible of sending a request to the API and retrieving the returned data from the API's response. Exception handling is also used here.
        /// Code was provided from Rapid API, and amended - original code contained in the comment block after this method.
        /// </summary>
        public async Task<CovidDataModel> GetCOVID19Statistics(CovidDataModel covidDataModelObject)
        {
            DataAccess dataAccessObject = new DataAccess();
            string APIResponse = await dataAccessObject.GetSerializedDataFromAPIAsync();
            if (APIResponse != "Data Retrieval from API Failed")
            {
                


                // An object (based on the API's JSON object root is created, containing a list of COVID19Statistics with each containing a list of Region data.

                Covid19StatisticsRoot covid19StatisticsRootObject = new Covid19StatisticsRoot();
                DeserializedCOVID19Statistics deserializedCOVID19StatisticsObject = new DeserializedCOVID19Statistics();
                covid19StatisticsRootObject = deserializedCOVID19StatisticsObject.DeserializeCOVID19Statistics(APIResponse);

                // Populate the data model object from the deserialized JSON object
                covidDataModelObject.ActiveCasesFromAPI = covid19StatisticsRootObject.data[0].active;
                covidDataModelObject.ConfirmedCases = covid19StatisticsRootObject.data[0].confirmed;

                // API appears to return Active cases + Confirmed Cases, thus a mathemical operation/subtraction is performed to get the current active cases
                covidDataModelObject.CalculatedActiveCases = (covidDataModelObject.ConfirmedCases - covidDataModelObject.ActiveCasesFromAPI);
                covidDataModelObject.ConfirmedDeaths = covid19StatisticsRootObject.data[0].deaths;


                // The Culture Info of my machine is en-MT, however, the last updated date from the API is en-US. DateTime.Parse was used to ensure that the Date is always converted to the desired/requested culure (dd/mm/yy),
                // en-GB (English Great Britian is used).
                var cultureInfo = new CultureInfo("en-GB");
                covidDataModelObject.DateLastUpdated = DateTime.Parse(covid19StatisticsRootObject.data[0].last_update, cultureInfo);

                // For testing & debugging purposes
                Console.WriteLine("Confirmed Cases = " + covidDataModelObject.ConfirmedCases);
                Console.WriteLine("Active Cases From API = " + covidDataModelObject.ActiveCasesFromAPI);
                Console.WriteLine("Calculated Active Cases = " + covidDataModelObject.CalculatedActiveCases);
                Console.WriteLine("Original Updated Date from API = " + covidDataModelObject.DateLastUpdated);
                Console.WriteLine("Confirmed Deaths = " + covidDataModelObject.ConfirmedDeaths);
                Console.WriteLine(CultureInfo.CurrentCulture);

                return covidDataModelObject;
            }
            else
            {
                covidDataModelObject.APIInteractionResult = false;
                return covidDataModelObject;
            }
        }

        /*
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
        */
    }
}
