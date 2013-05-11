using System.Web.Mvc;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using MagStore.Web.Models;

namespace MagStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShop shop;

        public HomeController(IShop shop)
        {
            this.shop = shop;
        }

        public ActionResult Index()
        {
            var user = (User) Session["CurrentUser"];
            var homeViewModel = new HomeViewModel {UserId = user.Id};
            return View(homeViewModel);
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}
