namespace ApplicationStore.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationStoreDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedCategories(context);
        }

        private void SeedRoles(ApplicationStoreDbContext context)
        {
            if (!context.Roles.Any())
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var adminRole = new IdentityRole(DbConstants.RoleAdmin);
                var developerRole = new IdentityRole(DbConstants.RoleDeveloper);

                manager.Create(adminRole);
                manager.Create(developerRole);
            }
        }

        private void SeedUsers(ApplicationStoreDbContext context)
        {
            if (!context.Users.Any())
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);

                var adminUser = new User()
                {
                    Email = DbConstants.AdminMail
                };

                manager.Create(adminUser, DbConstants.AdminPass);
                manager.AddToRole(adminUser.Id, DbConstants.RoleAdmin);
            }
        }

        private void SeedCategories(ApplicationStoreDbContext context)
        {
            if (!context.Categories.Any())
            {
                var games = new Category()
                {
                    Name = DbConstants.CategoryGames
                };

                var productivity = new Category()
                {
                    Name = DbConstants.CategoryProductivity
                };

                var entertainment = new Category()
                {
                    Name = DbConstants.CategoryEntertainment
                };

                context.Categories.Add(games);
                context.Categories.Add(productivity);
                context.Categories.Add(entertainment);

                context.SaveChanges();
            }
        }
    }
}
