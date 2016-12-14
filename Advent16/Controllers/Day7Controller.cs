using Advent16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Advent16.Controllers
{
    public class Day7Controller : Controller
    {
        // GET: Day7
        public ActionResult Index()
        {
            IPAddressesInput input = new IPAddressesInput();
            return View(input);
        }

        [HttpPost]
        public ActionResult CountTLSAddresses(IPAddressesInput input)
        {
            List<string> addresses = input.Addresses.Split(' ').ToList();
            List<IPAddress> formattedAddresses = addresses.Select(x => new IPAddress(x)).ToList();
            CountIP count = new CountIP();
            count.AddressCount = formattedAddresses.Where(x => x.SupportsTLS == true).Count();
            return PartialView("Count", count);
        }

        [HttpPost]
        public ActionResult CountSSLAddresses(IPAddressesInput input)
        {
            List<string> addresses = input.Addresses.Split(' ').ToList();
            List<IPAddress> formattedAddresses = addresses.Select(x => new IPAddress(x)).ToList();
            CountIP count = new CountIP();
            count.AddressCount = formattedAddresses.Where(x => x.SupportsSSL == true).Count();

            return PartialView("Count", count);
        }
    }
}