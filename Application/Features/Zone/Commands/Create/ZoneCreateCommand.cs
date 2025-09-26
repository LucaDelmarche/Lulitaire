using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Zone.Commands.Create;

public class ZoneCreateCommand
{
    public string Name { get; set; }
    [BindNever] // Ne lie pas ce champ depuis la requête
    [SwaggerSchema(ReadOnly = true)] 
    public string? user_id { get; set; }
}