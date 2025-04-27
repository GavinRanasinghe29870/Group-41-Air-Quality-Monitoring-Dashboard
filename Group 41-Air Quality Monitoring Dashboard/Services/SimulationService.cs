using Microsoft.Extensions.Hosting;
using Group_41_Air_Quality_Monitoring_Dashboard.Data;
using Group_41_Air_Quality_Monitoring_Dashboard.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Services
{
    public class SimulationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Random _random = new Random();
        public static bool IsSimulationRunning = false;
        public static int FrequencyMinutes = 5;
        public SimulationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (IsSimulationRunning)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        var sensors = await context.Sensors.ToListAsync();

                        foreach (var sensor in sensors)
                        {
                            var aqiData = new AQIData
                            {
                                SensorId = sensor.Id,
                                Timestamp = DateTime.UtcNow,
                                AQIValue = _random.Next(50, 150)
                            };

                            context.AQIData.Add(aqiData);
                        }

                        await context.SaveChangesAsync();
                    }

                    await Task.Delay(TimeSpan.FromMinutes(FrequencyMinutes), stoppingToken);
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }

}
