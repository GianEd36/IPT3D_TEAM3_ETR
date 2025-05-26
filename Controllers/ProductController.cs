using ETR_IPT3D_TEAM3.Helpers;
using ETR_IPT3D_TEAM3.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace ETR_IPT3D_TEAM3.Controllers
{
    public class ProductController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public ProductController(IConfiguration configuration)
        {
            _dbHelper = new DatabaseHelper(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            var products = new List<Product>();

            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT Id, Name, Price, Description, ImageUrl, CategoryId FROM Products";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            Price = reader.GetDecimal("Price"),
                            Description = reader.GetString("Description"),
                            ImageUrl = reader.GetString("ImageUrl"),
                            CategoryId = reader.GetInt32("CategoryId")
                        });
                    }
                }
            }

            return View(products);
        }

        public void AddProduct(Product product)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Products (Name, Price, Description, ImageUrl, CategoryId) VALUES (@name, @price, @description, @imageUrl, @categoryId)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@price", product.Price);
                    command.Parameters.AddWithValue("@description", product.Description);
                    command.Parameters.AddWithValue("@imageUrl", product.ImageUrl);
                    command.Parameters.AddWithValue("@categoryId", product.CategoryId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public IActionResult Search(string query)
        {
            var products = new List<Product>();

            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                string sqlQuery = "SELECT Name, Price, ImageUrl FROM Products WHERE Name LIKE @query";
                using (var command = new MySqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@query", "%" + query + "%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Name = reader.GetString("Name"),
                                Price = reader.GetDecimal("Price"),
                                ImageUrl = reader.GetString("ImageUrl")
                            });
                        }
                    }
                }
            }

            return Json(products);
        }


    }
}
