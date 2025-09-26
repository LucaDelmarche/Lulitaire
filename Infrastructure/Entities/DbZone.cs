using Infrastructure.Repository;

namespace Infrastructure.Entities;

public class DbZone : IHasId
{
    public int Id { get; set; }
    public int Id_user { get; set; }
    public string Name { get; set; }
}