using System.ComponentModel.DataAnnotations;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Models
{
    public class AQIAlertThreshold
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; } // e.g., "Moderate", "Unhealthy"

        [Required]
        public int MinValue { get; set; } // Minimum AQI value for the category

        [Required]
        public int MaxValue { get; set; } // Maximum AQI value for the category

        [Required]
        public string AlertMessage { get; set; } // Message to display for this category
    }
}