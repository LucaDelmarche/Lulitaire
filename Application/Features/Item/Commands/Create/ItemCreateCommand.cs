using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Item.Commands.Create;

public class ItemCreateCommand
{
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public string ExpirationDate { get; set; }
    public int ZoneId { get; set; }
    [BindNever] // Ne lie pas ce champ depuis la requête
    [SwaggerSchema(ReadOnly = true)] 
    public string? user_id { get; set; }
    public string Location { get; set; }
}