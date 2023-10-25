using Games.Entities;
using Games.Repositories;

namespace Games.EndPoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndPointName = "GetGame";
        
        public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
        {
            InMemGamesRepository repository = new ();

            var group = routes.MapGroup("/games")
                              .WithParameterValidation();

            #region endpoints
            // GET all the games 
            group.MapGet("/", () => repository.GetAll());

            // GET games By Id with verification if it exists
            group.MapGet("/{id}", (int id) =>
            {
                Game? game = repository.GetById(id);
                return game is not null ? Results.Ok(game) : Results.NotFound();
                
            }).WithName(GetGameEndPointName);

            // POST for creating a new game
            group.MapPost("/", (Game game) =>
            {
                repository.Create(game);
                return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
            });

            // PUT Update an existing game by ID
            group.MapPut("/{id}", (int id, Game updatedGame) =>
            {
                Game? existingGame = repository.GetById(id);

                if (existingGame is null) return Results.NotFound();

                // Update the existing game with the new data
                existingGame.Name = updatedGame.Name;
                existingGame.Genre = updatedGame.Genre;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.ImageUri = updatedGame.ImageUri;

                repository.Update(existingGame);
                return Results.NoContent();
            });

            // Delete a game by ID
            group.MapDelete("/{id}", (int id) =>
            {
                Game? gameToRemove = repository.GetById(id);

                return gameToRemove is not null ? Results.Ok(gameToRemove) : Results.NotFound();
             

            });
            #endregion

            return group;
        }
    }
}
