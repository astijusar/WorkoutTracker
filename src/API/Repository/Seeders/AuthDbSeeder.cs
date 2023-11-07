using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Repository.Seeders
{
    public class AuthDbSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthDbSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
        }

        private async Task AddAdminUser()
        {
            var admin = new User
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdmin = await _userManager.FindByNameAsync(admin.UserName);

            if (existingAdmin == null)
            {
                var createAdminResult = await _userManager.CreateAsync(admin, _configuration["AdminPassword"]);

                if (createAdminResult.Succeeded)
                    await _userManager.AddToRolesAsync(admin, UserRoles.All);
            }
        }

        private async Task AddDefaultRoles()
        {
            foreach (var role in UserRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);

                if (!roleExists)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
