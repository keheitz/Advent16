using Advent16.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Advent16.Tests.Controllers
{
    [TestClass]
    public class Day6ControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            Day6Controller controller = new Day6Controller();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Testing first line of example problem with both methods

        [TestMethod]
        public void DecodeRepitition()
        {
            //Arrange
            var controller = new Day6Controller();
            Models.SantaComm comm = new Models.SantaComm();
            comm.Message = "eedadn drvtee eandsr raavrd atevrs tsrnev sdttsa rasrtv nssdts ntnada svetve tesnvt vntsnd vrdear dvrsen enarar";
            // Act
            ActionResult result = controller.DecodeRepetition(comm);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Models.CorrectedMessage));
            Models.CorrectedMessage model = (Models.CorrectedMessage)viewResult.Model;
            Assert.AreEqual("easter", model.Message);
        }

        [TestMethod]
        public void DecodeModifiedReptition()
        {
            var controller = new Day6Controller();
            Models.SantaComm comm = new Models.SantaComm();
            comm.Message = "eedadn drvtee eandsr raavrd atevrs tsrnev sdttsa rasrtv nssdts ntnada svetve tesnvt vntsnd vrdear dvrsen enarar";

            // Act
            ActionResult result = controller.DecodeModifiedRepetition(comm);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(Models.CorrectedMessage));
            Models.CorrectedMessage model = (Models.CorrectedMessage)viewResult.Model;
            Assert.AreEqual("advent", model.Message);
        }
    }
}
