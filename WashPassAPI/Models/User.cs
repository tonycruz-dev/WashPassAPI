using Microsoft.AspNetCore.Identity;

namespace WashPassAPI.Models;

public class User : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
    public string? Location { get; set; }
}
