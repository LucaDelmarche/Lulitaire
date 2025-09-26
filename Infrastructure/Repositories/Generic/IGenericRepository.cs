using Infrastructure.Entities;
using Infrastructure.Repository;

namespace Infrastructure.Repositories.Generic;

public interface IGenericRepository<T> where T : class, IHasId
{
    T? GetById(int id); // Obtenir par ID
    IQueryable<T?> GetAll();
    // IEnumerable<DbEntity?> GetEntityByType(int type);
    T? Add(T? entity);
    
        
    bool ExistsById(int id);
    void DeleteById(int id);
    void Save(T entityToUpdate);
    
    
    
    T? Update(T? entityToUpdate);

    public bool FindUserByIdBool(int commandEntityId);
    void Delete(T? entity);
}