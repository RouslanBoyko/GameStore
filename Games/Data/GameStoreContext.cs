using Games.Entities;
using Microsoft.EntityFrameworkCore;

namespace Games.Data;

public class GameStoreContext : DbContext
{
	public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {
	}

    public DbSet<Game> Games => Set<Game>();
}
