using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Models
{
    public class AQIData
    {
        [Key]
        public int Id { get; set; }

        public int SensorId { get; set; }

        [ForeignKey("SensorId")]
        public Sensor Sensor { get; set; }

        public DateTime Timestamp { get; set; }

        public int AQIValue { get; set; }
    }
}
