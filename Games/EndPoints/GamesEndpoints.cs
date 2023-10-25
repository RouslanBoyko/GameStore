using Games.Entities;

namespace Games.EndPoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndPointName = "GetGame";
        static List<Game> games = new()
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
        public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/games")
                           .WithParameterValidation();

            #region endpoints
            // GET all the games 
            group.MapGet("/", () => games);

            // GET games By Id with verification if it exists
            group.MapGet("/{id}", (int id) =>
            {
                Game? game = games.Find(g => g.Id == id);

                if (game is null) return Results.NotFound();
                return Results.Ok(game);
            }).WithName(GetGameEndPointName);

            // POST for creating a new game
            group.MapPost("/", (Game game) =>
            {
                game.Id = games.Max(g => g.Id) + 1;
                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
            });

            // PUT Update an existing game by ID
            group.MapPut("/{id}", (int id, Game updatedGame) =>
            {
                Game? existingGame = games.Find(g => g.Id == id);

                if (existingGame is null) return Results.NotFound();

                // Update the existing game with the new data
                existingGame.Name = updatedGame.Name;
                existingGame.Genre = updatedGame.Genre;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.ImageUri = updatedGame.ImageUri;

                return Results.NoContent();
            });

            // Delete a game by ID
            group.MapDelete("/{id}", (int id) =>
            {
                Game? gameToRemove = games.Find(g => g.Id == id);

                if (gameToRemove is not null)
                {
                    games.Remove(gameToRemove);
                    return Results.NoContent();
                }
                return Results.NotFound();

            });
            #endregion

            return group;
        }
    }
}
