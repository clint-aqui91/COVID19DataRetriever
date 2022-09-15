using System.ComponentModel;

namespace COVID19DataRetriever.Models
{
    public class CovidDataModel
    {
        public DateTime DateFromAPI { get; set; }

        [DisplayName("Active Cases")]
        public int ActiveCases { get; set; }

        [DisplayName("Date Updated")]
        public DateTime ConvertedDate { get; set; }

        [DisplayName("Confirmed Cases till today")]
        public int ConfirmedCases { get; set; }

        [DisplayName("Death to Confirmed Cases Ratio")]
        public float DeathRatio { get; set; }

       // public static implicit operator CovidDataModel(Task<CovidDataModel> v)
      //  {
       //     throw new NotImplementedException();
      //  }
    }
}
