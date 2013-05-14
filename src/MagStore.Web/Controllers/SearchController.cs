using AutoMapper;
using MagStore.Entities;
using MagStore.Indexes;
using MagStore.Web.Models.Product;
using Raven.Client;
using System.Linq;
using System.Web.Mvc;

namespace MagStore.Web.Controllers
{
    public class SearchController : Controller
    {
        readonly IDocumentSession documentSession;

        public SearchController(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public ActionResult Index(string searchText)
        {
            var products = documentSession.Query<Products_FullText.Result, Products_FullText>().Search(x => x.Fields, searchText).As<Product>().ToArray();
            var viewModels = products.Select(Mapper.DynamicMap<SearchProductViewModel>);
            return View(viewModels);
        }
    }
}