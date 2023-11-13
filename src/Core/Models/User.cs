using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class User : IdentityUser
    {
        public bool ForceRelogin { get; set; }
        public ICollection<Workout>? Workouts { get; set; }
    }
}
