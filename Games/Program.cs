using Games.EndPoints;
using Games.Repositories;

namespace Games;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();
        var connString = builder.Configuration.GetConnectionString("GameStoreContext");
        var app = builder.Build();
        app.MapGamesEndpoints();
        app.Run();
    }
}