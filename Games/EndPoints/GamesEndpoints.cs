using Games.Entities;
using Games.Repositories;

namespace Games.EndPoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {


        var group = routes.MapGroup("/games")
                          .WithParameterValidation();

        #region endpoints
        // GET all the games 
        group.MapGet("/", async (IGamesRepository repository) =>
           (await repository.GetAllAsync()).Select(game => game.AsDto()));

        // GET games By Id with verification if it exists
        group.MapGet("/{id}", async (IGamesRepository repository, int id) =>
        {
            Game? game = await repository.GetByIdAsync(id);
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();

        }).WithName(GetGameEndPointName);

        // POST for creating a new game
        group.MapPost("/", async (IGamesRepository repository, CreateGameDto gameDto) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };



            await repository.CreateAsync(game);
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        // PUT Update an existing game by ID
        group.MapPut("/{id}", async (IGamesRepository repository, int id, UpdateGameDto updatedGameDto) =>
        {
            Game? existingGame = await repository.GetByIdAsync(id);

            if (existingGame is null) return Results.NotFound();

            // Update the existing game with the new data
            existingGame.Name = updatedGameDto.Name;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.Price = updatedGameDto.Price;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
            existingGame.ImageUri = updatedGameDto.ImageUri;

            await repository.UpdateAsync(existingGame);
            return Results.NoContent();
        });

        // Delete a game by ID
        group.MapDelete("/{id}", async (IGamesRepository repository, int id) =>
        {
            Game? gameToRemove = await repository.GetByIdAsync(id);

            if (gameToRemove is not null)
            {
                await repository.DeleteAsync(id);
                return Results.NoContent();
            }
            return Results.NotFound();


        });
        #endregion

        return group;
    }
}
