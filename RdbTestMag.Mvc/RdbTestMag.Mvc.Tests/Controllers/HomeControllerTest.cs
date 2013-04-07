using System;
using System.Web.Mvc;
using MagStore;
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
        public void Index(Guid? id)
        {
            // Arrange
            var controller = BuildHomeController();

            // Act
            var result = controller.Index(id) as ViewResult;

            // Assert
            if ( result == null ) return;
            var viewData = result.ViewData;
            Assert.AreEqual( "Welcome to ASP.NET MVC!", viewData[ "Message" ] );
        }

        private static HomeController BuildHomeController()
        {
            var store = Substitute.For<Store>();
            var controller = new HomeController( store );
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
            Assert.IsNotNull( result );
        }
    }
}
