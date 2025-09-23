using Newtonsoft.Json;

namespace Application.user.commands.PatchUsername;

public class UserPatchRoleCommand
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; }
}