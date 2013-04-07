using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MagStore;
using MagStore.Mvc;

namespace RdbTestMag.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.IgnoreRoute( "{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" } );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes( RouteTable.Routes );

            var ravenCredentials = new RavenCredentials
            {
                ApiKey = "b23f5154-30b0-48f8-b619-76ee77d0234d",
                Url = "https://ec2-eu4.cloudbird.net/databases/c818ddc6-dc4b-4b57-a439-4329fff0e61b.rdbtest-mag"
            };
            var store = new Store( ravenCredentials );
            Application["Store"] = store;

            ControllerBuilder.Current.SetControllerFactory( typeof( CustomControllerFactory ) );
        }
    }
}