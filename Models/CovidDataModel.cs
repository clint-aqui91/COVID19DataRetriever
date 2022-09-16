using System.ComponentModel;

namespace COVID19DataRetriever.Models
{
    public class CovidDataModel
    {
        [DisplayName("Date Last Updated")]
        public DateTime DateLastUpdated { get; set; }

        [DisplayName("Active Cases From API")]
        public int ActiveCasesFromAPI { get; set; }

        [DisplayName("Calculated Active Cases")]
        public int CalculatedActiveCases { get; set; }

       // [DisplayName("Date Updated")]
       // public DateTime ConvertedDate { get; set; }

        [DisplayName("Confirmed Cases till today")]
        public int ConfirmedCases { get; set; }

        [DisplayName("Number of Confirmed Deaths")]
        public int ConfirmedDeaths { get; set; }

        [DisplayName("Death to Confirmed Cases Ratio")]
        public float DeathRatio { get; set; }

        public bool APIInteractionResult { get; set; } = true;

       // public static implicit operator CovidDataModel(Task<CovidDataModel> v)
      //  {
       //     throw new NotImplementedException();
      //  }
    }
}
