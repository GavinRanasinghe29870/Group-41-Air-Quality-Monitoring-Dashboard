using System.Data.Entity;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("MySqlConnection") { }

        public DbSet<User> Users { get; set; }
    }
}
