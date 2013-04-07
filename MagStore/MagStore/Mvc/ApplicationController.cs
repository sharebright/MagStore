using System.Web.Mvc;
using MagStore.Data.Interfaces;

namespace MagStore.Mvc
{
    public class ApplicationController : Controller
    {
        protected IRepository repository;

        public ApplicationController(IRepository repository)
        {
            this.repository = repository;
        }
    }
}