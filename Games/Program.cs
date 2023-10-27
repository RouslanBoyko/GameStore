using Games.Data;
using Games.EndPoints;

namespace Games;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRepositories(builder.Configuration);
        var app = builder.Build();
        await app.Services.InitializeDbAsync();
        app.MapGamesEndpoints();
        app.Run();
    }
}