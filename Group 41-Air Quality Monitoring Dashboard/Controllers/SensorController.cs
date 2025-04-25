using Microsoft.AspNetCore.Mvc;
using Group_41_Air_Quality_Monitoring_Dashboard.Data;
using Group_41_Air_Quality_Monitoring_Dashboard.Models;
//using System;
//using System.Linq;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Controllers
{
    public class SensorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SensorController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SensorId,Location,Latitude,Longitude")] Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
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

                    return View();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving sensor or AQI data: " + ex.Message);
                }
            }
            return View(sensor);
        }
    }
}
