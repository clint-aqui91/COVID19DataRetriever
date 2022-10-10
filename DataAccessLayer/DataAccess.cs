namespace COVID19DataRetriever.DataAccessLayer
{
    public class DataAccess
    {
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
                    string? DataFromAPI = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(DataFromAPI);
                    return DataFromAPI;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                string? FailedAPIInteractionResult = "Data Retrieval from API Failed";
                return FailedAPIInteractionResult;
            }
        }
    }
}
