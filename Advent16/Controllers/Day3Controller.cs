using Advent16.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advent16.Library;

namespace Advent16.Controllers
{

    public class Day3Controller : Controller
    {
        // GET: Day3
        public ActionResult Index()
        {
            DesignDocument doc = new DesignDocument();
            doc.Part1 = true; //default to part 1, allow user to select part 2 later
            return View(doc);
        }

        [HttpPost]
        public ActionResult CountTriangles(DesignDocument doc)
        {
            TriangleCount count = new TriangleCount();
            if(doc.Part1 == true)
            {
                count.TotalValidTriangles = FindPart1Solution(doc);
            }
            else
            {
                count.TotalValidTriangles = FindPart2Solution(doc);
            }
            
            return PartialView("TriangleCount", count);
        }

        private int FindPart1Solution(DesignDocument doc)
        {
            List<Triangle> triangleSpecs = ParseTriangleSpecs(doc.TrianglesList);
            int count = triangleSpecs.Where(x => x.ValidTriangle == true).Count();

            return count;
        }

        private int FindPart2Solution(DesignDocument doc)
        {
            List<Triangle> triangleSpecs = ParseTriangleSpecs(doc.TrianglesList);
            List<Triangle> correctedTriangleSpecs = GetVerticalTriangles(triangleSpecs);
            int count = correctedTriangleSpecs.Where(x => x.ValidTriangle == true).Count();

            return count;
        }

        private List<Triangle> GetVerticalTriangles(List<Triangle> triangleSpecs)
        {
            List<Triangle> correctedTriangleSpecs = new List<Triangle>();
            var groupedSpecs = Common.Chunk(triangleSpecs, 3);
            foreach(var group in groupedSpecs)
            {
                List<int> measurements = group.Select(x => x.Measurement1).ToList();
                Triangle triangle = new Triangle(measurements);
                List<int> measurements2 = group.Select(x => x.Measurement2).ToList();
                Triangle triangle2 = new Triangle(measurements2);
                List<int> measurements3 = group.Select(x => x.Measurement3).ToList();
                Triangle triangle3 = new Triangle(measurements3);
                correctedTriangleSpecs.Add(triangle);
                correctedTriangleSpecs.Add(triangle2);
                correctedTriangleSpecs.Add(triangle3);
            }

            return correctedTriangleSpecs;
        }

        private List<Triangle> ParseTriangleSpecs(HttpPostedFileBase trianglesList)
        {
            List<Triangle> triangleSpecs = new List<Triangle>();

            var fileName = Path.GetFileName(trianglesList.FileName + "_" + DateTime.Now.ToString());
            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            trianglesList.SaveAs(path);
            // Read in lines from file.
            foreach (string line in System.IO.File.ReadLines(path))
            {
                line.Trim(); //remove beginning and end whitespace
                string[] measurements = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //remove emptry entries to account for varying spaces

                List<int> m = new List<int>();
                m.Add(int.Parse(measurements[0]));
                m.Add(int.Parse(measurements[1]));
                m.Add(int.Parse(measurements[2]));

                Triangle triangle = new Triangle(m);
                triangleSpecs.Add(triangle);
            }

            return (triangleSpecs);
        }
    }
}