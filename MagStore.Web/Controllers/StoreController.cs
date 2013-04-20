using System.Web.Mvc;
using RavenDbMembership.Infrastructure;

namespace MagStore.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly Shop shop;

        public StoreController(Shop shop)
        {
            this.shop = shop;
        }

        public ActionResult Manage()
        {
            return View();
        }

    }
}
