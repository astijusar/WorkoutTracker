using Microsoft.AspNetCore.Identity;

namespace Repository.Models
{
    public class User : IdentityUser
    {
        public bool ForceRelogin { get; set; }
    }
}
