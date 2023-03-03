using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace WeatherApp.Commands
{
    public class SqlDatabase : IDatabase
    {
        private readonly string connectionString = "Server=localhost;Database=WeatherApp;User Id=BeagleStreet;Password=Password;";
        private readonly ILogger<SqlDatabase> logger;

        public SqlDatabase(ILogger<SqlDatabase> logger)
        {
            this.logger = logger;
        }


        public async Task<bool> DeleteFavourite(int id)
        {
            string deleteQuery = "DELETE FROM Favourites WHERE id = (@Id)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    logger.LogInformation("Opening database connection");
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    logger.LogInformation("Executing SQL query: {0}", command.CommandText);
                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    logger.LogInformation("Closing database connection");
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while executing SQL query");
                throw;
            }


        }

        public async Task<string> GetFavourite(int id)
        {
            string getQuery = "SELECT location FROM Favourites WHERE id = (@Id)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    logger.LogInformation("Opening database connection");
                    SqlCommand command = new SqlCommand(getQuery, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    logger.LogInformation("Executing SQL query: {0}", command.CommandText);
                    connection.Open();
                    var result = await command.ExecuteScalarAsync();
                    logger.LogInformation("Closing database connection");
                    return (result == null) ? "" : result.ToString();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while executing SQL query");
                throw;
            }

        }

        public async Task<int> AddFavourite(string location)
        {
            string insertQuery = "INSERT INTO Favourites (location) VALUES (@Location); SELECT CAST(scope_identity() AS int)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    logger.LogInformation("Opening database connection");
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@Location", location);
                    logger.LogInformation("Executing SQL query: {0}", command.CommandText);
                    connection.Open();
                    var result = (int)await command.ExecuteScalarAsync();
                    logger.LogInformation("Closing database connection");
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while executing SQL query");
                throw;
            }

        }

        public async Task<bool> UpdateFavourite(int id, string location)
        {
            throw new NotImplementedException();
        }
    }
}
