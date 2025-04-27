using Group_41_Air_Quality_Monitoring_Dashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Controllers
{
    [Route("simulation")]
    public class SimulationController : Controller
    {
        public class StartSimulationRequest
        {
            public int Frequency { get; set; }
        }

        [HttpGet("status")]
        public IActionResult GetSimulationStatus()
        {
            return Ok(new
            {
                isRunning = SimulationService.IsSimulationRunning,
                frequency = SimulationService.FrequencyMinutes
            });
        }

        [HttpPost("start")]
        public IActionResult StartSimulation([FromBody] StartSimulationRequest request)
        {
            SimulationService.FrequencyMinutes = request.Frequency;
            SimulationService.IsSimulationRunning = true;
            return Ok(new { message = "Simulation started with frequency " + request.Frequency + " minutes." });
        }

        [HttpPost("stop")]
        public IActionResult StopSimulation()
        {
            SimulationService.IsSimulationRunning = false;
            return Ok(new { message = "Simulation stopped." });
        }
    }
}
