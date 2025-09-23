using Newtonsoft.Json;

namespace Application.user.commands.PatchUsername;

public class UserPatchEmailAddressCommand
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; }
    public string Email { get; set; }
}