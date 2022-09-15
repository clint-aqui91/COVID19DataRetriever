namespace COVID19DataRetriever.Models
{
    public class JsonDeserializedObjectModel
    {
        public string? date { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
        public int confirmed_diff { get; set; }
        public int deaths_diff { get; set; }
        public int recovered_diff { get; set; }
        public string? last_update { get; set; }
        public int active { get; set; }
        public int active_diff { get; set; }
        public float fatality_rate { get; set; }
        public string[]? region { get; set; }
        //public string? iso; // Country ISO Code
        //  public string? name; //Country Name
        //  public string? province;
        //  public string? lat;
        //public string long;
        //     public string? cities;
    }
}
