using Domain;

namespace Application.user.queries.getAll;

public class ItemGetallOutput
{
    public List<ItemDto> Items { get; set; } = new List<ItemDto>();
    public class ItemDto
    {
        public int Id { get; set; }
        public int Id_user { get; set; }
        public string Name { get; set; }  
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string ExpirationDate { get; set; }
        public int ZoneId { get; set; }
    }
}

