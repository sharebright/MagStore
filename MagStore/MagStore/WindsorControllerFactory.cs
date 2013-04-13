using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;
using Castle.Windsor;

namespace MagStore
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404,
                                        string.Format("The controller for path '{0}' could not be found.",
                                                      requestContext.HttpContext.Request.Path));
            }
            return (IController) kernel.Resolve(controllerType);
        }
    }

    namespace Advanced
    {
        public class WindsorControllerFactory : DefaultControllerFactory
        {
            private readonly IWindsorContainer container;

            /// <summary>
            /// Creates a new instance of the <see cref="WindsorControllerFactory"/> class.
            /// </summary>
            /// <param name="container">The Windsor container instance to use when creating controllers.</param>
            public WindsorControllerFactory(IWindsorContainer container)
            {
                if (container == null)
                {
                    throw new ArgumentNullException("container");
                }
                this.container = container;
            }

            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (controllerType == null)
                {
                    throw new HttpException(404,
                                            string.Format(
                                                "The controller for path '{0}' could not be found or it does not implement IController.",
                                                requestContext.HttpContext.Request.Path));
                }

                return (IController) container.Resolve(controllerType);
            }

            public override void ReleaseController(IController controller)
            {
                var disposable = controller as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }

                container.Release(controller);
            }
        }
    }
}