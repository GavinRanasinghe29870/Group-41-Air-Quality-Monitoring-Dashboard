using Microsoft.AspNetCore.Mvc;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
