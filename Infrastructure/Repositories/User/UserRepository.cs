using Infrastructure.Entities;
using Infrastructure.Repositories.User;

namespace Infrastructure.Repository;

public class UserRepository: GenericRepository<DbUser>, IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context) : base(context)
    {        
        _context = context;
       
    }


    public DbUser? GetByUsernameOrEmail(string usernameOrEmail)
    {
        return _context.Users.FirstOrDefault(x => x.Username == usernameOrEmail || x.Mail == usernameOrEmail);
    }
}