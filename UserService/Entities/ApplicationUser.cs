using Microsoft.AspNetCore.Identity;

namespace UserService.Entities;

public class ApplicationUser : IdentityUser
{
    public object? Meta { get; set; }
    public bool isDeleted { get; set; }

}