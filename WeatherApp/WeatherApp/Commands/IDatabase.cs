namespace WeatherApp.Commands
{
    public interface IDatabase
    {
        int AddFavourite(string location);
        string GetFavourite(int id);
        void UpdateFavourite(int id, string location);
        void DeleteFavourite(int id);        
    }
}
