using Advent16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Advent16.Controllers
{
    public class Day8Controller : Controller
    {
        // GET: Day8
        public ActionResult Index()
        {
            EncodedInstructions instructions = new EncodedInstructions();
            return View(instructions);
        }

        [HttpPost] //shows the number of lit pixels, along with the actual screen display for part 2
        public ActionResult DisplayScreen(EncodedInstructions instructions)
        {
            instructions.Input = HttpUtility.UrlDecode(instructions.Input);
            AuthenticationScreen screen = new AuthenticationScreen();
            foreach(InstructionStep step in instructions.StepsFromInput)
            {
                screen.UpdateScreen(step);
            }

            FinalDisplay display = new FinalDisplay(screen.Pixels);
            return PartialView("Display", display);
        }
    }
}