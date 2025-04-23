using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Group_41_Air_Quality_Monitoring_Dashboard.Controllers
{
    public class AccountController : Controller
    {
        // Update your actual connection string
        private readonly string _connectionString = "server=localhost;database=MyAppDb;user=root;password=yourpassword;";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM Users WHERE Username = @u", conn);
            cmd.Parameters.AddWithValue("@u", username);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var dbPassword = reader["Password"].ToString();
                if (password == dbPassword)
                {
                    // You can add session logic here
                    // HttpContext.Session.SetString("Username", username);

                    return RedirectToAction("Index", "Users");
                }
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }
    }
}
