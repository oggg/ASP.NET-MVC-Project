using System.Web.Mvc;
using ApplicationStore.Web.Areas.Administration.Controllers;

namespace ApplicationStore.Web.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administration",
                "Administration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new { controller = "Users" },
                new[] { "ApplicationStore.Web.Areas.Administration.Controllers" }
            );
        }
    }
}