using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group_41_Air_Quality_Monitoring_Dashboard.Data;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Controllers
{
    public class DashboardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardsController(ApplicationDbContext context)
        {
            _context = context;
        }

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

        // --- API Endpoints for AJAX ---

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetSensors()
        {
            var sensors = await _context.Sensors
                .Select(s => new {
                    s.SensorId,
                    s.Location,
                    s.Latitude,
                    s.Longitude,
                    LatestAQI = s.AQIDataRecords
                        .OrderByDescending(a => a.Timestamp)
                        .Select(a => (int?)a.AQIValue) // make it nullable
                        .FirstOrDefault() ?? 0 // if no data, use 0
                })
                .ToListAsync();

            return Json(sensors);
        }


        [HttpGet]
        public async Task<IActionResult> GetSensorHistory(string sensorId)
        {
            if (!int.TryParse(sensorId, out int sensorIdInt))
            {
                return BadRequest("Invalid sensor id");
            }

            var history = await _context.AQIData
                .Where(a => a.SensorId == sensorIdInt)
                .OrderByDescending(a => a.Timestamp)
                .Take(7)
                .Select(a => a.AQIValue)
                .ToListAsync();

            history.Reverse(); // oldest to newest
            return Json(history);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCurrentAQI()
        {
            var currentAQI = await _context.Sensors
                .Select(s => new {
                    s.Location,
                    LatestAQI = s.AQIDataRecords
                        .OrderByDescending(a => a.Timestamp)
                        .FirstOrDefault().AQIValue
                })
                .ToListAsync();

            return Json(currentAQI);
        }
    }
}
