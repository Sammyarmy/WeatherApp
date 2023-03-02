using System.Data.SqlClient;

namespace WeatherApp.Commands
{
    public class SqlDatabase : IDatabase
    {
        private readonly string connectionString = "Server=localhost;Database=WeatherApp;User Id=BeagleStreet;Password=Password;";

        public bool DeleteFavourite(int id)
        {
            string deleteQuery = "DELETE FROM Favourites WHERE id = (@Id)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var result = command.ExecuteScalar();
                return result != null;
            }
        }

        public string GetFavourite(int id)
        {
            string getQuery = "SELECT location FROM Favourites WHERE id = (@Id)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(getQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var result = command.ExecuteScalar();
                return (result == null) ? "" : result.ToString();
            }
        }

        public int AddFavourite(string location)
        {
            string insertQuery = "INSERT INTO Favourites (location) VALUES (@Location); SELECT CAST(scope_identity() AS int)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Location", location);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public bool UpdateFavourite(int id, string location)
        {
            throw new NotImplementedException();
        }
    }
}
