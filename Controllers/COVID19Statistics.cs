using Microsoft.AspNetCore.Mvc;

namespace COVID19DataRetriever.Controllers
{
    public class COVID19Statistics : Controller
    {
        public IActionResult Statistics()
        {
            return View();
        }
    }
}
