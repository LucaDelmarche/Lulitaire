using Infrastructure.Repository;

namespace Infrastructure.Entities;

public class DbItem : IHasId
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ExpritationData { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public int ZoneId { get; set; }
    public int UserId { get; set; }
}