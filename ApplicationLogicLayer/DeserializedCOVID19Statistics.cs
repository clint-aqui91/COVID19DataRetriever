using Newtonsoft.Json;

namespace COVID19DataRetriever.ApplicationLogicLayer
{
    public class DeserializedCOVID19Statistics
    {
        /// <summary>
        /// Class <c>DeserializedCOVID19Statistics</c> contains three sub-classes matching the JSON object's hierarchy and a method to deserialize the JSON object and returns it.
        /// 3 classes to match the JSON object returned from the API. Classes are generated using the https://json2csharp.com/ online tool, from the string (serialized JSON) returned from the API.
        /// </summary>

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
        /// Method <c>DeserializeCOVID19Statistics</c> deserializes the serialized JSON object using the Newtonsoft.Json library and retuns it.
        /// JSON Deserialization library source: Newtonsoft.Json, reference: https://www.newtonsoft.com/json & https://www.nuget.org/packages/Newtonsoft.Json/
        /// </summary>
        public Covid19StatisticsRoot DeserializeCOVID19Statistics (string serializedAPIData)
        {
            Covid19StatisticsRoot covid19StatisticsRootObject = new Covid19StatisticsRoot();
            covid19StatisticsRootObject = JsonConvert.DeserializeObject<Covid19StatisticsRoot>(serializedAPIData);
            return covid19StatisticsRootObject;
        }
    }
}
