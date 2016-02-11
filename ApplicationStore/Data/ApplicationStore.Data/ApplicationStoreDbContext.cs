using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ApplicationStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationStore.Data
{
    public class ApplicationStoreDbContext : IdentityDbContext<User>, IApplicationStoreDbContext
    {
        public ApplicationStoreDbContext()
            : base("ApplicationStoreConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public virtual IDbSet<Application> Applications { get; set; }

        public virtual IDbSet<AppImage> Images { get; set; }

        public virtual IDbSet<AppPath> Paths { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public static ApplicationStoreDbContext Create()
        {
            return new ApplicationStoreDbContext();
        }
    }
}
