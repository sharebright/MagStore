using System.Web.Mvc;
using MagStore.Entities;
using MagStore.Web.Models;

namespace MagStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var user =  GetCurrentUser();
            var homeViewModel = new HomeViewModel { UserId = user.Id };
            return View(homeViewModel);
        }

        private User GetCurrentUser()
        {
            return Session["CurrentUser"] as User;
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