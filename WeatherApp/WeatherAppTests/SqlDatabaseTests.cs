using System.Data.SqlClient;
using NUnit.Framework;
using WeatherApp.Commands;

namespace WeatherApp.Tests.Commands
{
    public class SqlDatabaseTests
    {
        private IDatabase _database;
        private string _connectionString = "Server=localhost;Database=WeatherApp;User Id=BeagleStreet;Password=Password;";

        [SetUp]
        public void Setup()
        {
            _database = new SqlDatabase();
        }

        [Test]
        public void AddFavourite_ShouldReturnId()
        {
            // Arrange
            string location = "London";

            // Act
            int id = _database.AddFavourite(location);

            // Assert
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string getQuery = "SELECT location FROM Favourites WHERE id = (@Id)";
                SqlCommand command = new SqlCommand(getQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var result = command.ExecuteScalar();
                Assert.AreEqual(location, result);
            }
        }

        [Test]
        public void GetFavourite_WithValidId_ShouldReturnLocation()
        {
            // Arrange
            int id = 1;
            string expectedLocation = "London";

            // Act
            string result = _database.GetFavourite(id);

            // Assert
            Assert.AreEqual(expectedLocation, result);
        }

        [Test]
        public void GetFavourite_WithInvalidId_ShouldReturnEmptyString()
        {
            // Arrange
            int id = -1;

            // Act
            string result = _database.GetFavourite(id);

            // Assert
            Assert.AreEqual("", result);
        }
    }
}