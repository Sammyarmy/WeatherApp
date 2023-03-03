using System.Data.SqlClient;

namespace WeatherApp.Commands
{
    public class SqlDatabase : IDatabase
    {
        private readonly string connectionString = "Server=localhost;Database=WeatherApp;User Id=BeagleStreet;Password=Password;";

        public async Task<bool> DeleteFavourite(int id)
        {
            string deleteQuery = "DELETE FROM Favourites WHERE id = (@Id)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        
        }

        public async Task<string> GetFavourite(int id)
        {
            string getQuery = "SELECT location FROM Favourites WHERE id = (@Id)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(getQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var result = await command.ExecuteScalarAsync();
                return (result == null) ? "" : result.ToString();
            }
        }

        public async Task<int> AddFavourite(string location)
        {
            string insertQuery = "INSERT INTO Favourites (location) VALUES (@Location); SELECT CAST(scope_identity() AS int)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Location", location);
                connection.Open();
                return (int)await command.ExecuteScalarAsync();
            }
        }

        public async Task<bool> UpdateFavourite(int id, string location)
        {
            throw new NotImplementedException();
        }
    }
}
