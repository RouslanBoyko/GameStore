using Games.Data;
using Games.EndPoints;
using Games.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Games;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();

        var connString = builder.Configuration.GetConnectionString("GameStoreContext");
        builder.Services.AddSqlServer<GameStoreContext>(connString);

        var app = builder.Build();


        app.Services.InitializeDb();
        app.MapGamesEndpoints();
        app.Run();
    }
}