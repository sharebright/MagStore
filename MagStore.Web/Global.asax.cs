using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MagStore.Entities;
using MagStore.Entities.Enums;
using MagStore.Mvc;

namespace MagStore.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Application["Store"] = new Store(new RavenCredentials
                {
                    ApiKey = "b23f5154-30b0-48f8-b619-76ee77d0234d",
                    Url = "https://ec2-eu4.cloudbird.net/databases/c818ddc6-dc4b-4b57-a439-4329fff0e61b.rdbtest-mag"
                });
            ControllerBuilder.Current.SetControllerFactory(typeof(CustomControllerFactory));

            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}