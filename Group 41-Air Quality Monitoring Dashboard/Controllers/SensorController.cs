// SensorController.cs
using Microsoft.AspNetCore.Mvc;
using Group_41_Air_Quality_Monitoring_Dashboard.Data;
using Group_41_Air_Quality_Monitoring_Dashboard.Models;
using System.Linq;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Controllers
{
    public class SensorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SensorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Controller Code (SensorController.cs)
        public IActionResult Create()
        {
            ViewBag.Sensors = _context.Sensors.ToList();  // This ensures you have the updated sensor list
            return View(new Sensor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SensorId,Location,Latitude,Longitude")] Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    sensor.Status = SensorStatus.Active;
                    _context.Sensors.Add(sensor);
                    _context.SaveChanges();

                    var aqiData = new AQIData
                    {
                        SensorId = sensor.Id,
                        Timestamp = DateTime.Now,
                        AQIValue = new Random().Next(50, 150)
                    };

                    _context.AQIData.Add(aqiData);
                    _context.SaveChanges();

                    ViewBag.Message = "Sensor and initial AQI data added successfully!";
                    ModelState.Clear();
                    ViewBag.Sensors = _context.Sensors.ToList();  // Ensure the sensors list is updated
                    return View("Create", new Sensor());  // Stay on the same page
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving sensor or AQI data: " + ex.Message);
                }
            }

            ViewBag.Sensors = _context.Sensors.ToList();
            return View(sensor);
        }


        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var sensor = _context.Sensors.FirstOrDefault(s => s.Id == id);
            if (sensor != null)
            {
                sensor.Status = sensor.Status == SensorStatus.Active ? SensorStatus.Inactive : SensorStatus.Active;
                _context.SaveChanges();
            }

            ViewBag.Sensors = _context.Sensors.ToList();
            return View("Create", new Sensor());
        }
    }
}
