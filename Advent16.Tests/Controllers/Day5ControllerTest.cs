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
    public class Day5ControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            Day2Controller controller = new Day2Controller();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Testing first line of example problem with both methods

        [TestMethod]
        public void GetPassword()
        {
            //Arrange
            var controller = new Day5Controller();
            Models.DoorInput input = new Models.DoorInput();
            input.DoorID = "abc";
            // Act
            ActionResult result = controller.GetPassword(input);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Models.DoorPassword));
            Models.DoorPassword model = (Models.DoorPassword)viewResult.Model;
            Assert.AreEqual("18F47A30", model.Password);
        }

        [TestMethod]
        public void GetSecondPassword()
        {
            var controller = new Day5Controller();
            Models.DoorInput input = new Models.DoorInput();
            input.DoorID = "abc";

            // Act
            ActionResult result = controller.GetSecondPassword(input);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Models.DoorPassword));
            Models.DoorPassword model = (Models.DoorPassword)viewResult.Model;
            Assert.AreEqual("05ACE8E3", model.Password);
        }
    }
}
