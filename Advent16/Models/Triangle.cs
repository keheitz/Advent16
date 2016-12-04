using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Advent16.Models
{
    public class Triangle
    {
        public int Measurement1 { get; set; }

        public int Measurement2 { get; set; }

        public int Measurement3 { get; set; }

        public bool ValidTriangle
        {
            get
            {
                bool isValid = false;
                if ((Measurement1 + Measurement2 > Measurement3) &&
                        (Measurement2 + Measurement3 > Measurement1) &&
                        (Measurement1 + Measurement3 > Measurement2))
                    isValid = true;

                return isValid;
            }
        }

        public Triangle(List<int> measurements)
        {
            Measurement1 = measurements[0];
            Measurement2 = measurements[1];
            Measurement3 = measurements[2];
        }
    }

    public class DesignDocument
    {
        [Display(Name="Solution Count")]
        public bool Part1 { get; set; }

        [Display(Name ="Design Document")]
        [Required]
        public HttpPostedFileBase TrianglesList { get; set; }
    }

    public class TriangleCount
    {
        public int TotalValidTriangles { get; set; }
    }
}