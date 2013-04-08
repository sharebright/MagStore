using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MagStore.Entities;
using MagStore.Entities.Enums;

namespace MagStore.Mvc
{
    public class MagController : Controller
    {
        public MagController()
        {
            var user = (User)System.Web.HttpContext.Current.Session["CustomerId"];
            if (user != null) return;
            var cart = new Cart
                {
                    CartId = Guid.NewGuid(),
                    OrderLines = new List<OrderLine>(),
                    Promotions = new List<Promotion>()
                };


            System.Web.HttpContext.Current.Session["CurrentUser"] = new User
                {
                    Id = Guid.NewGuid(),
                    AccountLevel = AccountLevel.Customer,
                    AccountStatus = AccountStatus.Restricted,
                    Create = DateTime.Now,
                    AgreedToMarketing = true,
                    ShoppingCart = cart,
                };
        }
    }
}