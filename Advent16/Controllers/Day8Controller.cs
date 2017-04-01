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
        // GET: Day7
        public ActionResult Index()
        {
            EncodedInstructions instructions = new EncodedInstructions();
            return View(instructions);
        }

        [HttpPost]
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

        //[HttpPost]
        //public ActionResult CountSSLAddresses(IPAddressesInput input)
        //{
        //    List<string> addresses = input.Addresses.Split(' ').ToList();
        //    List<IPAddress> formattedAddresses = addresses.Select(x => new IPAddress(x)).ToList();
        //    CountIP count = new CountIP();
        //    count.AddressCount = formattedAddresses.Where(x => x.SupportsSSL == true).Count();

        //    return PartialView("Count", count);
        //}
    }
}