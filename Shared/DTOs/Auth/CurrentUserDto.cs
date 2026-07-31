namespace Shared.DTOs.Auth;

public class CurrentUserDto
{
    public string Id { get; set; } = "";

    public string Email { get; set; } = "";

    public List<string> Roles { get; set; } = [];
}