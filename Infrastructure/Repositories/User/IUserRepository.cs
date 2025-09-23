using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repository;

namespace Infrastructure.Repositories.User;

public interface IUserRepository:IGenericRepository<DbUser>
{
    public DbUser? GetByUsernameOrEmail(string usernameOrEmail);
    public DbUser? GetById(int id);
    
}