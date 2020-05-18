using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Piranha;
using Piranha.AspNetCore.Identity;
using Piranha.AspNetCore.Identity.Data;
using Piranha.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Identity
{
    public class IdentitySeed : IHeroesCupIdentitySeed
    {
        private readonly IDb db;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;


        public IdentitySeed(IDb db, UserManager<User> userManager, IConfiguration configuration)
        {
            this.db = db;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task SeedIdentityAsync()
        {
            await SeedRolesAsync();
            var username = this.configuration["Identity:Users:Timeheroes:Name"];
            var email = this.configuration["Identity:Users:Timeheroes:Email"];
            if (!this.db.Users.Any(u => u.Email == email && u.UserName == username))
            {
                var user = new User
                {
                    UserName = username,
                    Email = email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var createResult = await this.userManager.CreateAsync(user, "password");

                if (createResult.Succeeded)
                {
                    var timeheroesRoleName = this.configuration["Identity:Roles:TimeheroesRole"];
                    await this.userManager.AddToRoleAsync(user, timeheroesRoleName);
                }
            }

            await this.db.SaveChangesAsync();
        }

        public async Task SeedRolesAsync()
        {
            await SeedTimeheroesRoleAsync();
            await SeedCoordinatorRoleAsync();
        }

        private async Task SeedTimeheroesRoleAsync()
        {
            var timeheroesRoleName = this.configuration["Identity:Roles:TimeheroesRole"];
            if (!this.db.Roles.Any(r => r.Name == timeheroesRoleName || r.NormalizedName == timeheroesRoleName.ToUpper()))
            {
                var timeheroesRole = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = timeheroesRoleName,
                    NormalizedName = timeheroesRoleName.ToUpper()
                };

                this.db.Roles.Add(timeheroesRole);
                AddPermissions(timeheroesRole, GetTimeheroesPermissions());
            }


            await this.db.SaveChangesAsync();
        }

        private async Task SeedCoordinatorRoleAsync()
        {
            var coordinatorRoleName = this.configuration["Identity:Roles:CoordinatorRole"];
            if (!this.db.Roles.Any(r => r.Name == coordinatorRoleName || r.NormalizedName == coordinatorRoleName.ToUpper()))
            {
                var coordinatorRole = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = coordinatorRoleName,
                    NormalizedName = coordinatorRoleName.ToUpper()
                };

                this.db.Roles.Add(coordinatorRole);
                AddPermissions(coordinatorRole, GetCoordinatorPermissions());
            }

            await this.db.SaveChangesAsync();
        }

        private void AddPermissions(Role role, IEnumerable<PermissionItem> permissions)
        {
            foreach (var permission in permissions)
            {
                var roleClaim = this.db.RoleClaims.FirstOrDefault(c =>
                    c.RoleId == role.Id && c.ClaimType == permission.Name && c.ClaimValue == permission.Name);
                if (roleClaim == null)
                {
                    this.db.RoleClaims.Add(new IdentityRoleClaim<Guid>
                    {
                        RoleId = role.Id,
                        ClaimType = permission.Name,
                        ClaimValue = permission.Name
                    });
                }
            }
        }

        private IEnumerable<PermissionItem> GetCoordinatorPermissions()
        {
            var coordinatorPermissions = new HashSet<PermissionItem>();

            foreach (var permission in App.Permissions.GetPermissions())
            {
                var isCoordinatorPermission = permission.Name == ClubsModule.Permissions.Clubs ||
                    permission.Name == ClubsModule.Permissions.ClubsAdd ||
                    permission.Name == ClubsModule.Permissions.ClubsDelete ||
                    permission.Name == ClubsModule.Permissions.ClubsEdit ||
                    permission.Name == ClubsModule.Permissions.ClubsSave ||
                    permission.Name == ClubsModule.Permissions.Heroes ||
                    permission.Name == ClubsModule.Permissions.HeroesAdd ||
                    permission.Name == ClubsModule.Permissions.HeroesDelete ||
                    permission.Name == ClubsModule.Permissions.HeroesEdit ||
                    permission.Name == ClubsModule.Permissions.HeroesSave ||
                    permission.Name == ClubsModule.Permissions.HeroesAddCoordinator ||
                    permission.Name == Piranha.Manager.Permission.Admin;

                if (isCoordinatorPermission)
                {
                    coordinatorPermissions.Add(permission);
                }
            }

            return coordinatorPermissions;
        }

        private IEnumerable<PermissionItem> GetTimeheroesPermissions()
        {
            return App.Permissions.GetPermissions();
        }
    }
}