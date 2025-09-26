namespace Application.Features.Zone.Commands.PatchUsername;

public class ZonePatchCommand
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public string? user_id { get; set; }
    public string Name { get; set; }
}