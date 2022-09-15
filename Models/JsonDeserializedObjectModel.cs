namespace COVID19DataRetriever.Models
{
    public class JsonDeserializedObjectModel
    {
        public DateOnly date { get; set; }
        public int confirmed;
        public int deaths;
        public int recovered;
        public int confirmed_diff;
public int deaths_diff;
public int recovered_diff;
        public DateTime last_update;
        public int active;
        public int active_diff;
        public float fatality_rate;
        public string[] region;
        //public string? iso; // Country ISO Code
      //  public string? name; //Country Name
      //  public string? province;
      //  public string? lat;
//public string long;
       //     public string? cities;
    }
}
