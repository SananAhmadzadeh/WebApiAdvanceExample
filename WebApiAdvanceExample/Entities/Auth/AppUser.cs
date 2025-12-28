
using Microsoft.AspNetCore.Identity;

namespace WebApiAdvanceExample.Entities.Auth
{
    public class AppUser<Guid> : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
