namespace WeatherApp.Commands
{
    public interface IDatabase
    {
        int AddFavourite(string location);
        string GetFavourite(int id);
        bool UpdateFavourite(int id, string location);
        bool DeleteFavourite(int id);        
    }
}
