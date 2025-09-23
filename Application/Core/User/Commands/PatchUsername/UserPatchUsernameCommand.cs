using Newtonsoft.Json;

namespace Application.user.commands.PatchUsername;

public class UserPatchUsernameCommand
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; }
    public string Username { get; set; }
}