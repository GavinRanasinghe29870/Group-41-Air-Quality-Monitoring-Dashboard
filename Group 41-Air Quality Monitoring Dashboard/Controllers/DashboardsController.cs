using Microsoft.AspNetCore.Mvc;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Controllers
{
    public class DashboardsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AQIDashboard()
        {
            return View();
        }

        public IActionResult SystemDashboard()
        {
            return View();
        }


    }
}
