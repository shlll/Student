namespace Student.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Student.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Student.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Student.Models.ApplicationDbContext context)
        {
            var roleManager =
               new RoleManager<IdentityRole>(
                   new RoleStore<IdentityRole>(context));


            var userManager =
                new UserManager<ApplicationUser>(
                        new UserStore<ApplicationUser>(context));

            if (!context.Roles.Any(p => p.Name == "Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                roleManager.Create(adminRole);
            }

            if (!context.Roles.Any(p => p.Name == "Moderator"))
            {
                var moderatorRole = new IdentityRole("Moderator");
                roleManager.Create(moderatorRole);
            }

            ApplicationUser adminUser;
            ApplicationUser moderatorUser;

            if (!context.Users.Any(p => p.UserName == "admin@student.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "admin@student.com";
                adminUser.Email = "admin@student.com";

                userManager.Create(adminUser, "Password-1");
            }
            else
            {
                adminUser = context.Users.First(p => p.UserName == "admin@student.com");
            }

            if (!context.Users.Any(p => p.UserName == "moderator@student.com"))
            {
                moderatorUser = new ApplicationUser();
                moderatorUser.Email = "moderator@student.com";
                moderatorUser.UserName = "moderator@student.com";

                userManager.Create(moderatorUser, "Password-1`");
            }
            else
            {
                moderatorUser = context.Users.First(p => p.UserName == "moderator@student.com");
            }

            if (!userManager.IsInRole(adminUser.Id, "Admin"))
            {
                userManager.AddToRole(adminUser.Id, "Admin");
            }

            if (!userManager.IsInRole(moderatorUser.Id, "Moderator"))
            {
                userManager.AddToRole(moderatorUser.Id, "Moderator");
            }
        }
    }
}
