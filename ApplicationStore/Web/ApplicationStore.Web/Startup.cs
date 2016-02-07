using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApplicationStore.Web.Startup))]
namespace ApplicationStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
