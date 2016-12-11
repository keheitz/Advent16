using Advent16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Advent16.Controllers
{
    public class Day6Controller : Controller
    {
        // GET: Day6
        public ActionResult Index()
        {
            SantaComm comm = new SantaComm();
            return View(comm);
        }

        [HttpPost]
        public ActionResult GetCorrectedComm(SantaComm comm)
        {
            StringBuilder msg = new StringBuilder();
            for (int i = 0; i < comm.RepetitionCode.First().Length; i++)
            {
                List<LetterCount> count = comm.RepetitionCode.Select(x => x[i]).GroupBy(c => c).Select(x => new LetterCount
                {
                    Letter = x.Key,
                    Count = x.Count()
                }).ToList();
                LetterCount mostfrequent = count.OrderByDescending(x => x.Count).Take(1).First();
                msg.Append(mostfrequent.Letter);
            }
            CorrectedMessage correctcomm = new CorrectedMessage();
            correctcomm.Message = msg.ToString();
            return PartialView("Message", correctcomm);
        }

        [HttpPost]
        public ActionResult DecodeModifiedRepetition(SantaComm comm)
        {
            StringBuilder msg = new StringBuilder();
            for (int i = 0; i < comm.RepetitionCode.First().Length; i++)
            {
                List<LetterCount> count = comm.RepetitionCode.Select(x => x[i]).GroupBy(c => c).Select(x => new LetterCount
                {
                    Letter = x.Key,
                    Count = x.Count()
                }).ToList();
                LetterCount leastfrequent = count.OrderBy(x => x.Count).Take(1).First();
                msg.Append(leastfrequent.Letter);
            }
            CorrectedMessage correctcomm = new CorrectedMessage();
            correctcomm.Message = msg.ToString();
            return PartialView("Message", correctcomm);
        }

    }
}