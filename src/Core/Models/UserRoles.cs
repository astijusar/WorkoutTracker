﻿namespace Core.Models
{
    public static class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
        public const string PremiumUser = nameof(PremiumUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, User, PremiumUser };
    }
}
