using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IHasId
{
    private readonly AppDbContext _context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
    }

    public T? GetById(int id)
    {
        return DbSet.Find(id);
    }
    
    public IQueryable<T?> GetAll()
    {
        return DbSet.AsQueryable();
    }

    public T? Add(T? entity)
    {
        DbSet.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public T? Update(T? entity)
    {
        if (entity == null || !DbSet.Any(e => e.Id == entity.Id))
        {
            throw new InvalidOperationException("Entity not found or invalid.");
        }

        DbSet.Update(entity);
        _context.SaveChanges();
        return entity;
    }


    public void Delete(T? entity)
    {
        DbSet.Remove(entity);
        _context.SaveChanges();
    }
    
    public bool ExistsById(int id)
    {
        return DbSet.Any(e => e.Id == id);
    }
    

    public void DeleteById(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            Delete(entity);
        }
    }
    

    public void Save(T userToUpdate)
    {
        _context.Entry(userToUpdate).State = EntityState.Modified;
        _context.SaveChanges();
    }
    
    public bool FindUserByIdBool(int commandEntityId)
    {
        return _context.Users.FirstOrDefault(a=>a.Id == commandEntityId) != null;
    }
    

}