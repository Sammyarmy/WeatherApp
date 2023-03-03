using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WeatherApp.Commands;

namespace WeatherApp.Tests.Commands
{
    public class SqlDatabaseTests
    {
        private IDatabase _database;
        private string _connectionString = "Server=localhost;Database=WeatherApp;User Id=BeagleStreet;Password=Password;";
        private Mock<ILogger<SqlDatabase>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<SqlDatabase>>();
            _database = new SqlDatabase(_loggerMock.Object);
        }

        [Test]
        public async Task AddFavourite_ShouldReturnId()
        {
            // Arrange
            string location = "London";

            // Act
            int id = await _database.AddFavourite(location);

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
        public async Task GetFavourite_WithValidId_ShouldReturnLocation()
        {
            // Arrange
            int id = 1;
            string expectedLocation = "London";

            // Act
            string result = await _database.GetFavourite(id);

            // Assert
            Assert.AreEqual(expectedLocation, result);
        }

        [Test]
        public async Task GetFavourite_WithInvalidId_ShouldReturnEmptyString()
        {
            // Arrange
            int id = -1;

            // Act
            string result = await _database.GetFavourite(id);

            // Assert
            Assert.AreEqual("", result);
        }
    }
}