using ETR_IPT3D_TEAM3.Helpers;
using ETR_IPT3D_TEAM3.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace ETR_IPT3D_TEAM3.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public CategoryController(IConfiguration configuration)
        {
            _dbHelper = new DatabaseHelper(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            var categories = new List<Category>();

            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT Id, Name FROM Categories";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name")
                        });
                    }
                }
            }

            return View(categories);
        }
    }
}
