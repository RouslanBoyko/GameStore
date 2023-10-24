using Games.Entities;
namespace Games
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string GetGameEndPointName = "GetGame";
            List<Game> games = new()
            {
                        new Game()
                        {
                            Id = 1,
                            Name = "The Witcher 3",
                            Genre = "Rpg",
                            Price = 30M,
                            ReleaseDate = new DateTime(2015, 05, 31),
                            ImageUri = "https://placehold.co/100"
                        },

                        new Game()
                        {
                            Id = 2,
                            Name = "The Elder Scrolls V: Skyrim",
                            Genre = "Rpg",
                            Price = 30M,
                            ReleaseDate = new DateTime(2015, 05, 31),
                            ImageUri = "https://placehold.co/100"
                        },

                        new Game()
                        {
                            Id = 3,
                            Name = "Project Zomboid",
                            Genre = "Survival",
                            Price = 18M,
                            ReleaseDate = new DateTime(2012, 05, 31),
                            ImageUri = "https://placehold.co/100"
                        }


            };


            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // get all the games endpoint
            app.MapGet("/games", () => games);

            // get games By Id endpoint
            app.MapGet("/games/{id}", (int id) =>
            {
                Game? game = games.Find(g => g.Id == id);

                if (game == null) return Results.NotFound();
                return Results.Ok(game);
            }).WithName(GetGameEndPointName);

            // post for creating a new game
            app.MapPost("/games", (Game game) => 
            {
                game.Id = games.Max(g => g.Id) + 1;
                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
            });
            app.Run();
        }
    }
}