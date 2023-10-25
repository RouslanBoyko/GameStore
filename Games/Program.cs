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

            #region endpoints
            // GET all the games 
            app.MapGet("/games", () => games);

            // GET games By Id with verification if it exists
            app.MapGet("/games/{id}", (int id) =>
            {
                Game? game = games.Find(g => g.Id == id);

                if (game == null) return Results.NotFound();
                else return Results.Ok(game);
            }).WithName(GetGameEndPointName);

            // POST for creating a new game
            app.MapPost("/games", (Game game) =>
            {
                game.Id = games.Max(g => g.Id) + 1;
                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
            });

            // PUT Update an existing game by ID
            app.MapPut("/games/{id}", (int id, Game updatedGame) =>
            {
                Game? existingGame = games.Find(g => g.Id == id);

                if (existingGame == null)
                {
                    return Results.NotFound();
                }

                // Update the existing game with the new data
                existingGame.Name = updatedGame.Name;
                existingGame.Genre = updatedGame.Genre;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.ImageUri = updatedGame.ImageUri;

                return Results.Ok(existingGame);
            });

            // Delete a game by ID
            app.MapDelete("/games/{id}", (int id) =>
            {
                Game? gameToRemove = games.Find(g => g.Id == id);

                if (gameToRemove == null)
                {
                    return Results.NotFound();
                }

                games.Remove(gameToRemove);

                return Results.NoContent();
            });

            #endregion



            app.Run();
        }
    }
}