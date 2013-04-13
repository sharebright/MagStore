using System;
using System.Web;
using System.Web.Mvc;

namespace MagStore.Mvc
{
    public class MagViewMasterPage : ViewMasterPage
    {
        public Guid? Id { get; set; }
        public Shop Shop { get; set; }

        public MagViewMasterPage()
        {
            Id = HttpContext.Current.Session["CustomerId"] as Guid?;
            Shop = HttpContext.Current.Application["Shop"] as Shop;
        }
    }
}
