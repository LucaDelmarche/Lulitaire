namespace Application.Features.Item.Queries.GetAll;

public class ItemGetAllQuery
{
    public string id_user { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]

    public int id_zone { get; set; }
}