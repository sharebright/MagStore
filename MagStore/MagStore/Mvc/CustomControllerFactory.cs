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

            var store = HttpContext.Current.Application["Store"] as Store;
            //var repository = store.RavenRepository; // new RavenRepository(docStore);

            return Activator.CreateInstance(controllerType, store) as IController;
        }
    }
}