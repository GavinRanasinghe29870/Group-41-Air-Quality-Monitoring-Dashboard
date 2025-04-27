using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Models
{
    public enum SensorStatus
    {
        Active,
        Inactive
    }

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

        public SensorStatus Status { get; set; } = SensorStatus.Active;

        [InverseProperty("Sensor")]
        public ICollection<AQIData>? AQIDataRecords { get; set; }
    }
}
