using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Models
{
    public class Sensor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SensorId { get; set; }

        [Required]
        public string Location { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [InverseProperty("Sensor")]
        public ICollection<AQIData>? AQIDataRecords { get; set; } 
    }
}
