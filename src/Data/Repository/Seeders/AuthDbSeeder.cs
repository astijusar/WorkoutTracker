using Core.Models;
using Data.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

namespace Data.Repository.Seeders
{
    public class AuthDbSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<UserPasswordOptions> _options;
        private readonly ILogger<AuthDbSeeder> _logger;

        public AuthDbSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<AuthDbSeeder> logger,
            IOptions<UserPasswordOptions> options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _options = options;
            _logger = logger;
            _options = options;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
            await AddDemoUser();
            await AddTestUser();
        }

        private async Task AddTestUser()
        {
            var testUser = new User
            {
                UserName = "testUser",
                Email = "testuser@email.com"
            };

            var existingUser = await _userManager.FindByNameAsync(testUser.UserName);

            if (existingUser == null)
            {
                var createdUserResult = await _userManager.CreateAsync(testUser, "/TestUser321");//_configuration["TestUserPassword"]);

                if (!createdUserResult.Succeeded)
                {
                    _logger.LogError("Error when creating a test user!");
                    return;
                }

                await _userManager.AddToRolesAsync(testUser, UserRoles.All);
            }
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
                var createdUserResult = await _userManager.CreateAsync(demoUser, _options.Value.DemoUserPassword);

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
                var createAdminResult = await _userManager.CreateAsync(admin, _options.Value.AdminPassword);

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
