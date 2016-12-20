using System.Web.Http;

namespace MultiTenantRepositry.EF.Api.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}