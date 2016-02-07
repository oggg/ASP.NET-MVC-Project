using ApplicationStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationStore.Data
{
    public class ApplicationStoreDbContext : IdentityDbContext<User>
    {
        public ApplicationStoreDbContext()
            : base("ApplicationStoreConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationStoreDbContext Create()
        {
            return new ApplicationStoreDbContext();
        }
    }
}
