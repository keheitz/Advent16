﻿using Advent16.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Advent16.Tests.Controllers
{
    [TestClass]
    public class Day3ControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            Day3Controller controller = new Day3Controller();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Testing model
        //couldn't figure out how to test the action result with httppostedfilebase

        [TestMethod]
        public void InvalidTriangle()
        {
            //arrange
            List<int> measurements = new List<int> { 5, 10, 25 };
            var model = new Models.Triangle(measurements);
           
            //assert
            Assert.AreEqual(false, model.ValidTriangle);
        }

        [TestMethod]
        public void Triangle()
        {
            //arrange
            List<int> measurements = new List<int> { 7, 10, 5 };
            var model = new Models.Triangle(measurements);

            //assert
            Assert.AreEqual(true, model.ValidTriangle);
        }

    }
}
