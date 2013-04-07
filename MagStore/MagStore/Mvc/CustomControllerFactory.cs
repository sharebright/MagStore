using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MagStore.Data;
using Raven.Client.Document;

namespace MagStore.Mvc
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return base.GetControllerInstance(requestContext, controllerType);

            var docStore = HttpContext.Current.Application["DocumentStore"] as DocumentStore;
            var repository = new RavenRepository(docStore);

            return Activator.CreateInstance(controllerType, repository) as IController;
        }
    }
}