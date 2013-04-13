using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MagStore.Mvc
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return base.GetControllerInstance(requestContext, controllerType);

            var store = HttpContext.Current.Application["Shop"] as Shop;
            //var repository = Shop.RavenRepository; // new RavenRepository(docStore);

            return Activator.CreateInstance(controllerType, store) as IController;
        }
    }
}