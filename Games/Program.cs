using Games.Entities;

namespace Games
{
    public class Program
    {
        public static void Main(string[] args)
        {

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

            app.MapGet("/games", () => games);

            app.Run();
        }
    }
}