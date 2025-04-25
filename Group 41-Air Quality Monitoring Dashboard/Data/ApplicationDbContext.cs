using Microsoft.EntityFrameworkCore;
using Group_41_Air_Quality_Monitoring_Dashboard.Models;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
