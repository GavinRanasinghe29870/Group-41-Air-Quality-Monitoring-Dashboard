using Microsoft.AspNetCore.Mvc;
using Group_41_Air_Quality_Monitoring_Dashboard.Data;
using Group_41_Air_Quality_Monitoring_Dashboard.Models;
using System.Linq;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Controllers
{
    public class AQIAlertThresholdsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AQIAlertThresholdsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AQIAlertThresholds
        public IActionResult Index()
        {
            var thresholds = _context.AQIAlertThresholds.ToList();
            return View(thresholds);
        }

        // GET: AQIAlertThresholds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AQIAlertThresholds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AQIAlertThreshold threshold)
        {
            if (ModelState.IsValid)
            {
                _context.AQIAlertThresholds.Add(threshold);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(threshold);
        }

        // GET: AQIAlertThresholds/Edit/5
        public IActionResult Edit(int id)
        {
            var threshold = _context.AQIAlertThresholds.Find(id);
            if (threshold == null)
            {
                return NotFound();
            }
            return View(threshold);
        }

        // POST: AQIAlertThresholds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AQIAlertThreshold threshold)
        {
            if (ModelState.IsValid)
            {
                _context.AQIAlertThresholds.Update(threshold);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(threshold);
        }

        // GET: AQIAlertThresholds/Delete/5
        public IActionResult Delete(int id)
        {
            var threshold = _context.AQIAlertThresholds.Find(id);
            if (threshold == null)
            {
                return NotFound();
            }
            return View(threshold);
        }

        // POST: AQIAlertThresholds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var threshold = _context.AQIAlertThresholds.Find(id);
            if (threshold != null)
            {
                _context.AQIAlertThresholds.Remove(threshold);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}