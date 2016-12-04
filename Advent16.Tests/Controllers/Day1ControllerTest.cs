using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Advent16;
using Advent16.Controllers;


namespace Advent16.Tests.Controllers
{
    [TestClass]
    public class Day1ControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDistanceTest()
        {
            var controller = new Day1Controller();

            // Act
            ActionResult result = controller.GetDistance(new Models.Directions());

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Models.Distance));
            Models.Distance model = (Models.Distance)viewResult.Model;
            Assert.AreEqual(236, model.Blocks);
        }

        [TestMethod]
        public void GetFirstDuplicateLocationDistanceTest()
        {
            var controller = new Day1Controller();

            // Act
            ActionResult result = controller.GetFirstDuplicateLocationDistance(new Models.Directions());

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Models.Distance));
            Models.Distance model = (Models.Distance)viewResult.Model;
            Assert.AreEqual(182, model.Blocks);
        }

    }
}
