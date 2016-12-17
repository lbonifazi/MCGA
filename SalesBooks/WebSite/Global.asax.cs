using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSite
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Service Config
            ServiceConfig.RegisterConfig();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
