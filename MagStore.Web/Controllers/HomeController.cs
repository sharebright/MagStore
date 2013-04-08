using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagStore.Entities;
using MagStore.Mvc;
using MagStore.Web.Models;

namespace MagStore.Web.Controllers
{
    public class HomeController : MagController
    {
        private readonly Store store;

        public HomeController(Store store)
        {
            this.store = store;
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
