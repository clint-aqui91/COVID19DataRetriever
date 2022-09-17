using System.ComponentModel;

namespace COVID19DataRetriever.Models
{

    /// <summary>
    /// Class <c>CovidDataModel</c> is the data model class, which is used in the MVC. Each property holds a string value (DisplayName) which is used in the view's labels.
    /// </summary>
    public class CovidDataModel
    {
        // DateTime Value to hold the Date Last Updated (source from the API)
        [DisplayName("Date Last Updated")]
        public DateTime DateLastUpdated { get; set; }

        // Integer Value to hold the Active Cases value (sourced from the API)
        [DisplayName("Active Cases From API")]
        public int ActiveCasesFromAPI { get; set; }

        // Integer value to hold the Confirmed Active Cases (calculated in the controller method)
        [DisplayName("Calculated Active Cases")]
        public int CalculatedActiveCases { get; set; }

        // Integer value to hold the Confirmed Cases (sourced from the API)
        [DisplayName("Confirmed Cases till today")]
        public int ConfirmedCases { get; set; }

        // Integer value to hold the Confirmed Deaths (sourced from the API)
        [DisplayName("Number of Confirmed Deaths")]
        public int ConfirmedDeaths { get; set; }


        // float value to hold the Death Ratio
        [DisplayName("Death to Confirmed Cases Ratio")]
        public float DeathRatio { get; set; }

        // Boolean value which holds the success/fail result from the interaction with the API.
        public bool APIInteractionResult { get; set; } = true;

    }
}
