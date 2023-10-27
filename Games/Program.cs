using Games.Data;
using Games.EndPoints;

namespace Games;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRepositories(builder.Configuration);
        var app = builder.Build();
        app.Services.InitializeDb();
        app.MapGamesEndpoints();
        app.Run();
    }
}