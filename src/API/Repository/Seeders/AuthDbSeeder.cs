using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Repository.Seeders
{
    public class AuthDbSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthDbSeeder> _logger;

        public AuthDbSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, ILogger<AuthDbSeeder> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
            await AddDemoUser();
        }

        private async Task AddDemoUser()
        {
            var demoUser = new User
            {
                UserName = "demoUser",
                Email = "demouser@email.com"
            };

            var existingUser = await _userManager.FindByNameAsync(demoUser.UserName);

            if (existingUser == null)
            {
                var createdUserResult = await _userManager.CreateAsync(demoUser, _configuration["DemoUserPassword"]);

                if (!createdUserResult.Succeeded)
                {
                    _logger.LogError("Error when creating a demo user!");
                    return;
                }

                await _userManager.AddToRoleAsync(demoUser, UserRoles.User);
            }
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

                if (!createAdminResult.Succeeded)
                {
                    _logger.LogError("Error when creating an admin user!");
                    return;
                }

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
