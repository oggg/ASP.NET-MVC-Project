using System.Data.Entity;
using ApplicationStore.Data;
using ApplicationStore.Data.Migrations;

namespace ApplicationStore.Web.App_Start
{
    public class DbConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationStoreDbContext, Configuration>());
            ApplicationStoreDbContext.Create().Database.Initialize(true);
        }
    }
}
