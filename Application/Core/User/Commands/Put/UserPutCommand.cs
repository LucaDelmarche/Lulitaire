using System.Text.Json.Serialization;
using Domain;
using Infrastructure.Repository;

namespace Application.user.commands.put;

public class UserPutCommand : IHasId
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Username{get;set;}
    public string Mail{get;set;}
    public string Password{get;set;}
    public double Score{get;set;}
    public int Role{get;set;}
}