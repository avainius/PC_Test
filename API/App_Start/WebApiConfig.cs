//using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            DataAccess.DataManager.Initialize();
            Logger.Initialize();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Home",
                routeTemplate: "api/Home/{action}"
            );
        }
    }
}
