using System.Data.Entity;
using ApplicationStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationStore.Data
{
    public class ApplicationStoreDbContext : IdentityDbContext<User>, IApplicationstoreDbContext
    {
        public ApplicationStoreDbContext()
            : base("ApplicationStoreConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Application> Applications { get; set; }

        public virtual IDbSet<AppImage> Images { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public static ApplicationStoreDbContext Create()
        {
            return new ApplicationStoreDbContext();
        }
    }
}
