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
    public class Day2ControllerTest
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
        public void GetCode()
        {
            var controller = new Day2Controller();
            Models.CodeInstructions instructions = new Models.CodeInstructions();
            instructions.Instructions = "ULL";
            // Act
            ActionResult result = controller.GetCode(instructions);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Models.BathroomCode));
            Models.BathroomCode model = (Models.BathroomCode)viewResult.Model;
            Assert.AreEqual(1, model.Code.First());
        }

        [TestMethod]
        public void GetWeirdCode()
        {
            var controller = new Day2Controller();
            Models.CodeInstructions instructions = new Models.CodeInstructions();
            instructions.Instructions = "ULL";
            // Act
            ActionResult result = controller.GetWeirdoCode(instructions);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Models.WeirdoBathroomCode));
            Models.WeirdoBathroomCode model = (Models.WeirdoBathroomCode)viewResult.Model;
            Assert.AreEqual('5', model.Code.First());
        }
    }
}
