using System;
using System.Web;
using System.Web.Mvc;

namespace MagStore.Mvc
{
    public class MagViewMasterPage : ViewMasterPage
    {
        public Guid? Id { get; set; }
        public Store Store { get; set; }

        public MagViewMasterPage()
        {
            Id = HttpContext.Current.Session["CustomerId"] as Guid?;
            Store = HttpContext.Current.Application["Store"] as Store;
        }
    }
}
