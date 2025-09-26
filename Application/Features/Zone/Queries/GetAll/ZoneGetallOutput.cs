using Domain;

namespace Application.user.queries.getAll;

public class ZoneGetallOutput
{
    public List<ZoneDto> Zones { get; set; } = new List<ZoneDto>();
    public class ZoneDto
    {
        public int Id { get; set; }
        public int Id_user { get; set; }
        public string Name { get; set; }   
    }
}

