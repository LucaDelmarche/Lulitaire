namespace Application.Features.Item.Commands.Patch;

public class ItemPatchCommand
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public string? user_id { get; set; }
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public string ExpirationDate { get; set; }
    public string Location { get; set; }
}