using System.Web.Mvc;

namespace MagStore.Web.Controllers
{
    public class ShopController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult UpdateCloudbird()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult UpdateAzure()
        {
            throw new System.NotImplementedException();
        }
    }
}