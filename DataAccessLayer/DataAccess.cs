namespace COVID19DataRetriever.DataAccessLayer
{
    public class DataAccess
    {
        /// <summary>
        /// Method <c>GetSerializedDataFromAPIAsync</c> is the method responsible of sending a request to the API and retrieving the returned response from the API's. Exception handling is also used here.
        /// Replace the "-- Place API Key here -- with the API key acquired from RapidApi.com"
        /// </summary>
        public async Task<String> GetSerializedDataFromAPIAsync()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports?q=Malta&region_name=Malta&iso=MLT"),
                    Headers =
                {
                    { "X-RapidAPI-Key", "e286b44a29msh4c7a6448753438ap1f11a5jsn85743f38880b" },
                    { "X-RapidAPI-Host", "covid-19-statistics.p.rapidapi.com" },
                },
                };

                using (HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage))
                {
                    response.EnsureSuccessStatusCode();
                    string? APIResponseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(APIResponseContent);
                    return APIResponseContent;
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
