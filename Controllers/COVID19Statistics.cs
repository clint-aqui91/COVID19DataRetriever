using COVID19DataRetriever.ApplicationLogicLayer;
using COVID19DataRetriever.Models;
using Microsoft.AspNetCore.Mvc;

namespace COVID19DataRetriever.Controllers
{
    /// <summary>
    /// Class <c>COVID19Statistics</c> is the controller class representing the controller component of the web application's MVC.
    /// </summary>
    public class COVID19Statistics : Controller
    {
        /// <summary>
        /// Method <c>StatisticsAsync</c> is the controller action method responsible of handling the MVC's UI logic. It calls the Application Logic Layer to retrieve the data model object containing deserialized data
        /// acquired from the API, and returns the data model object to the respective view.
        /// </summary>
        public async Task<IActionResult> StatisticsAsync()
        {
            CovidDataModel covidDataModelObject = new CovidDataModel();
            ApplicationLogic applicationLogicObject = new ApplicationLogic();

            covidDataModelObject = await applicationLogicObject.GetCOVID19Statistics(covidDataModelObject);

            return View(covidDataModelObject);
        }

    }
}
