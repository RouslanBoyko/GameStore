using Games.Entities;

namespace Games.Repositories;


public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games = new()
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

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
         return await Task.FromResult(games);
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await Task.FromResult(games.Find(game => game.Id == id));
    }

    public async Task CreateAsync(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Game updatedGame)
    {
        var index = games.FindIndex(game => game.Id == updatedGame.Id);
        games[index] = updatedGame;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);
        await Task.CompletedTask;
    }
}
