using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Advent16.Models
{
    public class Directions
    {
        [Display(Name ="Sequence")]
        public string DirectionsSequence { get; set; } //comma delimited turn and distance instructions

        public Directions()
        {
            DirectionsSequence = "R3, L5, R1, R2, L5, R2, R3, L2, L5, R5, L4, L3, R5, L1, R3, R4, R1, L3, R3, L2, L5, L2, R4, R5, R5, L4, L3, L3, R4, R4, R5, L5, L3, R2, R2, L3, L4, L5, R1, R3, L3, R2, L3, R5, L194, L2, L5, R2, R1, R1, L1, L5, L4, R4, R2, R2, L4, L1, R2, R53, R3, L5, R72, R2, L5, R3, L4, R187, L4, L5, L2, R1, R3, R5, L4, L4, R2, R5, L5, L4, L3, R5, L2, R1, R1, R4, L1, R2, L3, R5, L4, R2, L3, R1, L4, R4, L1, L2, R3, L1, L1, R4, R3, L4, R2, R5, L2, L3, L3, L1, R3, R5, R2, R3, R1, R2, L1, L4, L5, L2, R4, R5, L2, R4, R4, L3, R2, R1, L4, R3, L3, L4, L3, L1, R3, L2, R2, L4, L4, L5, R3, R5, R3, L2, R5, L2, L1, L5, L1, R2, R4, L5, R2, L4, L5, L4, L5, L2, L5, L4, R5, R3, R2, R2, L3, R3, L2, L5";
        }
    }

    public class Distance
    {
        public int Blocks { get; set; }
    }

    public class DirectionElement
    {
        public char TurnOrientation { get; set; }

        public int NumBlocks { get; set; }
    }

    public class Coordinate
    {
        private Coordinate currentCoord;

        public int xval { get; set; }

        public int yval { get; set; }

        public Coordinate()
        {
            xval = 0;
            yval = 0;
        }

        public Coordinate(Coordinate currentCoord)
        {
            this.currentCoord = currentCoord;
        }
    }
}