namespace WeatherApp.Commands
{
    public interface IDatabase
    {
        Task<int> AddFavourite(string location);
        Task<string> GetFavourite(int id);
        Task<bool> UpdateFavourite(int id, string location);
        Task<bool> DeleteFavourite(int id);        
    }
}
