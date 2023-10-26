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
        group.MapGet("/", (IGamesRepository repository) =>
            repository.GetAll().Select(game => game.AsDto()));

        // GET games By Id with verification if it exists
        group.MapGet("/{id}", (IGamesRepository repository, int id) =>
        {
            Game? game = repository.GetById(id);
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();

        }).WithName(GetGameEndPointName);

        // POST for creating a new game
        group.MapPost("/", (IGamesRepository repository, CreateGameDto gameDto) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };



            repository.Create(game);
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        // PUT Update an existing game by ID
        group.MapPut("/{id}", (IGamesRepository repository, int id, UpdateGameDto updatedGameDto) =>
        {
            Game? existingGame = repository.GetById(id);

            if (existingGame is null) return Results.NotFound();

            // Update the existing game with the new data
            existingGame.Name = updatedGameDto.Name;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.Price = updatedGameDto.Price;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
            existingGame.ImageUri = updatedGameDto.ImageUri;

            repository.Update(existingGame);
            return Results.NoContent();
        });

        // Delete a game by ID
        group.MapDelete("/{id}", (IGamesRepository repository, int id) =>
        {
            Game? gameToRemove = repository.GetById(id);

            if (gameToRemove is not null)
            {
                repository.Delete(id);
                return Results.NoContent();
            }
            return Results.NotFound();


        });
        #endregion

        return group;
    }
}
