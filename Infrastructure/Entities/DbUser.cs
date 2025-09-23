using Infrastructure.Repository;

namespace Infrastructure.Entities;

public class DbUser : IHasId
{
    public int Id { get; set; }
        
    public string Username { get; set; }

    public string Mail { get; set; }

    public byte[] Password { get; set; }

    public int Role{ get; set; }
    
}