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
    public class Day4ControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            Day4Controller controller = new Day4Controller();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Testing models, bc can't figure out how to test method with httppostedfilebase

        [TestMethod]
        public void RoomName()
        {
            //arrange
            var model = new Models.RoomName("aaaaa-bbb-z-y-x-123[abxyz]");

            //assert
            Assert.AreEqual("abxyz", model.Checksum);
            Assert.AreEqual(123, model.SectorID);
            Assert.AreEqual(false, model.DecoyRoom);
            Assert.AreEqual("aaaaa-bbb-z-y-x", model.EncryptedRoomName);
        }

        [TestMethod]
        public void Decoder()
        {
            //arrange
            var model = new Models.Decoder();

            //assert
            char letter = model.FindDecodedLetter('q', 343);
            Assert.AreEqual('v', letter);
        }
    }
}
