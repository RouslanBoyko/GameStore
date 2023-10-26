using Games.Entities;
using System.Collections.Generic;

namespace Games.Repositories
{

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

        public IEnumerable<Game> GetAll()
        {
            return games;
        }

        public Game? GetById(int id)
        {
            return games.Find(game => game.Id == id);
        }

        public void Create(Game game)
        {
            game.Id = games.Max(game => game.Id) + 1;
            games.Add(game);
        }

        public void Update(Game updatedGame)
        {
            var index = games.FindIndex(game => game.Id == updatedGame.Id);
            games[index] = updatedGame;
        }

        public void Delete(int id)
        {
            var index = games.FindIndex(game => game.Id == id);
            games.RemoveAt(index);
        }
    }
}
