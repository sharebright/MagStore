using System.Web.Mvc;
using MagStore.Data;
using NSubstitute;
using NUnit.Framework;
using Raven.Client;
using RdbTestMag.Mvc.Controllers;

namespace RdbTestMag.Mvc.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            var controller = BuildHomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            if (result == null) return;
            var viewData = result.ViewData;
            Assert.AreEqual("Welcome to ASP.NET MVC!", viewData["Message"]);
        }

        private static HomeController BuildHomeController()
        {
            var ravenRepository = new RavenRepository(Substitute.For<IDocumentStore>());
            var controller = new HomeController(ravenRepository);
            return controller;
        }

        [Test]
        public void About()
        {
            // Arrange
            var controller = BuildHomeController();

            // Act
            var result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
