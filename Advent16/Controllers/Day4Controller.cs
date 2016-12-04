using Advent16.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Advent16.Controllers
{
    public class Day4Controller : Controller
    {
        // GET: Day4
        public ActionResult Index()
        {
            RoomListDocument list = new RoomListDocument();
            return View(list);
        }

        public ActionResult SumSectors(RoomListDocument list)
        {
            SectorIDSum sum = new SectorIDSum();

            if (list.Part1 == true)
            {
                sum.Total = FindPart1Solution(list);
            }
            else
            {
                sum.Total = FindPart2Solution(list);
            }

            return View("SectorsSum", sum);
        }
        
        private int FindPart1Solution(RoomListDocument list)
        {
            List<RoomName> roomnames = ParseRoomList(list.RoomList);
            int sum = roomnames.Where(x => x.DecoyRoom == false).Sum(x => x.SectorID);

            return sum;
        }

        private int FindPart2Solution(RoomListDocument list)
        {
            List<RoomName> roomnames = ParseRoomList(list.RoomList);
            List<RoomName> validrooms = roomnames.Where(x => x.DecoyRoom == false).ToList();
            RoomName name = validrooms.Where(x => x.DecryptedRoomName.Contains("northpole")).First();

            return name.SectorID;
        }

        private List<RoomName> ParseRoomList(HttpPostedFileBase rooms)
        {
            List<RoomName> roomNames = new List<RoomName>();

            var fileName = Path.GetFileName(rooms.FileName + "_" + DateTime.Now.ToString());
            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            rooms.SaveAs(path);
            // Read in lines from file.
            foreach (string line in System.IO.File.ReadLines(path))
            {
                line.Trim(); //remove beginning and end whitespace
                RoomName name = new RoomName(line);
                roomNames.Add(name);
            }

            return (roomNames);
        }
    }
}