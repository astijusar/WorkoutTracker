using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User : IdentityUser
    {
        public bool ForceRelogin { get; set; }
    }
}
