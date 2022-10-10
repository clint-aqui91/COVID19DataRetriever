using COVID19DataRetriever.DataAccessLayer;
using COVID19DataRetriever.Models;
using Newtonsoft.Json;
using System.Globalization;
using static COVID19DataRetriever.ApplicationLogicLayer.DeserializedCOVID19Statistics;

namespace COVID19DataRetriever.ApplicationLogicLayer
{
    /// <summary>
    /// Class <c>ApplicationLogic</c> contains the application logic code of the web application's back-end.
    /// layer.
    /// </summary>
    public class ApplicationLogic
    {
        /// <summary>
        /// Method <c>GetCOVID19Statistics</c> contains the application logic method which calls the data access layer to retrieve the COVID19 Statistics from the API, then deserializes it using the other class found in
        /// this layer.
        /// </summary>
        public async Task<CovidDataModel> GetCOVID19Statistics(CovidDataModel covidDataModelObject)
        {
            DataAccess dataAccessObject = new DataAccess();
            string APIResponse = await dataAccessObject.GetSerializedDataFromAPIAsync();

            // If API response does not contain string value denoting API interaction failure, deserialize the JSON object contained within the string, and return it to the calling method (being the MVC's controller
            // component).
            if (APIResponse != "Data Retrieval from API Failed")
            {
                // Deserialization of the JSON object
                Covid19StatisticsRoot covid19StatisticsRootObject = new Covid19StatisticsRoot();
                DeserializedCOVID19Statistics deserializedCOVID19StatisticsObject = new DeserializedCOVID19Statistics();
                covid19StatisticsRootObject = deserializedCOVID19StatisticsObject.DeserializeCOVID19Statistics(APIResponse);

                // Populate the data model object from the deserialized JSON object
                covidDataModelObject.ActiveCasesFromAPI = covid19StatisticsRootObject.data[0].active;
                covidDataModelObject.ConfirmedCases = covid19StatisticsRootObject.data[0].confirmed;

                // API appears to return Active cases + Confirmed Cases, thus a mathemical operation/subtraction is performed to get the current active cases
                covidDataModelObject.CalculatedActiveCases = (covidDataModelObject.ConfirmedCases - covidDataModelObject.ActiveCasesFromAPI);
                covidDataModelObject.ConfirmedDeaths = covid19StatisticsRootObject.data[0].deaths;

                /*
                 * The Culture Info of my machine is en-MT, however, the last updated date from the API is en-US. DateTime.Parse was used to ensure that the Date is always converted to the desired/requested culure
                 * (dd/mm/yy), en-GB (English Great Britian is used).
                */
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

            // Else, hence string returned from the data access layer denotes API interaction failure, set the boolean property found in the data model to false and return the data model object.
            else
            {
                covidDataModelObject.APIInteractionResult = false;
                return covidDataModelObject;
            }
        }

    }
}
