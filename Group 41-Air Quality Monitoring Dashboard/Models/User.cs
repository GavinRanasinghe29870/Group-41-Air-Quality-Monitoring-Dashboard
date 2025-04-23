using System.ComponentModel.DataAnnotations;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; }
    }
}
