using Domain;

namespace Application.user.commands.create;

public class ZoneCreateOutput
{
    public ZoneCreateDto zonedto;
    public class ZoneCreateDto
    {
        public string Name { get; set; }
    }
}