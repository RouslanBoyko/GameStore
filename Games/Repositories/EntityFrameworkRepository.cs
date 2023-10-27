using Games.Data;
using Games.Entities;
using Microsoft.EntityFrameworkCore;

namespace Games.Repositories;

public class EntityFrameworkRepository : IGamesRepository
{
    private readonly GameStoreContext dbContext;

    public EntityFrameworkRepository(GameStoreContext dbContext)
    {
        this.dbContext=dbContext;
    }

    public IEnumerable<Game> GetAll()
    {
        return dbContext.Games.AsNoTracking().ToList();
    }

    public Game? GetById(int id)
    {
        return dbContext.Games.Find(id);
    }
    public void Create(Game game)
    {
        dbContext.Games.Add(game);
        dbContext.SaveChanges();
    }

    public void Update(Game updatedGame)
    {
        dbContext.Update(updatedGame);
        dbContext.SaveChanges();
    }
    public void Delete(int id)
    {
        dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
    }
}
