using System;
using System.Web;
using System.Web.Mvc;

namespace MagStore.Mvc
{
    public class MagController : Controller
    {
        public MagController()
        {
            var customerId = System.Web.HttpContext.Current.Session[ "CustomerId" ] as Guid?;
            if ( !customerId.HasValue )
            {
                System.Web.HttpContext.Current.Session[ "CustomerId" ] = Guid.NewGuid();
            }

        }
    }
}