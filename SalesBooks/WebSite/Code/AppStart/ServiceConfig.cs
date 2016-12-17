using Services;
using System.Web;

namespace WebSite
{
    public class ServiceConfig
    {
        public static void RegisterConfig()
        {
            // Connection string (we need to have this first, to start logging)
            Config.Database.ConnectionString = AppInfo.WebConfig.ConnectionString;
        }
    }
}