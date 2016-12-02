﻿using Advent16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Advent16.Controllers
{
    /*
     * You're airdropped near Easter Bunny Headquarters in a city somewhere. 
     * "Near", unfortunately, is as close as you can get - 
     * the instructions on the Easter Bunny Recruiting Document the Elves intercepted start here, 
     * and nobody had time to work them out further.
     * 
     * The Document indicates that you should start at the given coordinates (where you just landed) and face North. 
     * Then, follow the provided sequence: either turn left (L) or right (R) 90 degrees, then walk forward the given number of blocks, 
     * ending at a new intersection.
     */

    public class Day1Controller : Controller
    {
        // GET: Day1 - Main view for parsing directions distance
        public ActionResult Index()
        {
            Directions directions = new Directions();
            return View(directions);
        }
        
        public ActionResult GetDistance(Directions directions)
        {
            List<DirectionElement> orderedDirections = ParseDirections(directions);
            List<Coordinate> journeyCoordinates = FindCoordinatePath(orderedDirections);
            Coordinate destination = journeyCoordinates.Last();

            ViewBag.distance = CalculateDistance(destination); 
            return PartialView("Distance");
        }
        
        public ActionResult GetFirstDuplicateLocationDistance(Directions directions)
        {
            List<DirectionElement> orderedDirections = ParseDirections(directions);

            //get ordered list of the visited coordinates
            List<Coordinate> journeyCoordinates = FindCoordinatePath(orderedDirections);
            Coordinate easterbunnyhq = FindFirstDuplicateCoordinate(journeyCoordinates);

            ViewBag.distance = CalculateDistance(easterbunnyhq); 
            return PartialView("Distance");
        }

        

        #region private
        //break list of steps into defined direction elements
        private List<DirectionElement> ParseDirections(Directions directions)
        {
            List<DirectionElement> orderedDirections = new List<DirectionElement>();
            //TODO add validation - currently making a ton of assumptions about format...
            List<String> steps = directions.DirectionsSequence.Split(',').ToList();
            orderedDirections = steps.Select(x => new DirectionElement {
                TurnOrientation = x.Trim()[0],
                NumBlocks = Int16.Parse(x.Trim().Substring(1,x.Trim().Length-1))
            }).ToList();

            return (orderedDirections);
        }

        private List<Coordinate> FindCoordinatePath(List<DirectionElement> orderedDirections)
        {
            Coordinate currentCoord = new Coordinate();
            bool yaxis = true; //we start facing north - this value tracks whether we are moving along y axis
            bool increaseCoord = true; //keeps track of whether you are moving in positive or negative direction along axis

            List<Coordinate> visitedCoordinates = new List<Coordinate>();

            int stepsProcessed = 0;
            foreach (DirectionElement step in orderedDirections)
            {
                if (stepsProcessed > 0)
                {
                    yaxis = !yaxis; //every step begins by turning 90 degrees right or left, so constantly chaging axis of movement
                    increaseCoord = FindCoordDirection(increaseCoord, yaxis, step.TurnOrientation);
                }
                currentCoord = UpdateCoord(currentCoord, increaseCoord, yaxis, step.NumBlocks);
                Coordinate processedCoord = new Coordinate();
                processedCoord.yval = currentCoord.yval;
                processedCoord.xval = currentCoord.xval;
                visitedCoordinates.Add(processedCoord);
                stepsProcessed++;
            }

            return visitedCoordinates;
        }

        //this is ugly, because I didn't initially realize that any intersection passed counted for part 2
        //(not just the destination of the step) - had to hack to GetAllIntersections and process those
        private Coordinate FindFirstDuplicateCoordinate(List<Coordinate> journeyCoordinates)
        {
            Coordinate easterbunnyhq = new Coordinate();
            List<Coordinate> processedCoords = new List<Coordinate>();
            processedCoords.Add(new Coordinate()); //by default, 0,0 is new coordinate - add start
            foreach(Coordinate coord in journeyCoordinates)
            {
                List<Coordinate> intersections = GetAllIntersections(processedCoords.LastOrDefault(), coord);
                foreach(Coordinate intersection in intersections)
                {
                    if (processedCoords.Any(x => x.xval == intersection.xval && x.yval == intersection.yval))
                    {
                        return intersection;
                    }
                    else
                    {

                        processedCoords.Add(coord);
                    }
                }
                if (processedCoords.Any(x => x.xval == coord.xval && x.yval == coord.yval))
                {
                    return coord;
                }
                else
                {
                    
                    processedCoords.Add(coord);
                }
            }
            return easterbunnyhq;
        }

        //refactor this later!!
        private List<Coordinate> GetAllIntersections(Coordinate processedCoord, Coordinate coord)
        {
            List<Coordinate> intersections = new List<Coordinate>();
            if(processedCoord.xval == coord.xval) //moving vertically
            {
                int startval = processedCoord.yval;
                if(processedCoord.yval > coord.yval)
                {
                    startval--;
                }
                else
                {
                    startval++;
                }
                foreach(int block in Range(startval, coord.yval))
                {
                    Coordinate intersection = new Coordinate();
                    intersection.xval = coord.xval;
                    intersection.yval = block;
                    intersections.Add(intersection);
                }
            }
            else
            {
                int startval = processedCoord.xval;
                if (processedCoord.xval > coord.xval)
                {
                    startval--;
                }
                else
                {
                    startval++;
                }
                foreach (int block in Range(startval, coord.xval))
                {
                    Coordinate intersection = new Coordinate();
                    intersection.xval = block;
                    intersection.yval = coord.yval;
                    intersections.Add(intersection);
                }
            }

            return intersections;
        }

        //Move this into common library - method for finding all numbers between two integers
        //(similar to python Range method)
        public static IEnumerable<int> Range(int start, int stop)
        {
            for (int i = start; i < stop; i++)
                yield return i;
        }

        private int CalculateDistance(Coordinate currentCoord)
        {
            Coordinate originalCoord = new Coordinate(); //initialized to 0,0 in constructor
            int distance = Math.Abs(originalCoord.xval - currentCoord.xval) + Math.Abs(originalCoord.yval - currentCoord.yval);

            return (distance);
        }

        private Coordinate UpdateCoord(Coordinate currentCoord, bool increaseCoord, bool yaxis, int numBlocks)
        {
            if (!increaseCoord)
                numBlocks = numBlocks * -1; //decreasing coord values- so get neg value for blocks
            //update appropriate x or y coordinate value
            if (yaxis)
                currentCoord.yval += numBlocks;
            else
                currentCoord.xval += numBlocks;

            return (currentCoord);
        }

        private bool FindCoordDirection(bool increaseCoord, bool yaxis, char turn)
        {
            if (increaseCoord)
            {
                if(yaxis) //moving up yaxis
                {
                    if (turn == 'R')
                        increaseCoord = true;
                    else if (turn == 'L')
                        increaseCoord = false;
                }
                else
                {
                    if (turn == 'R')
                        increaseCoord = false;
                    else if (turn == 'L')
                        increaseCoord = true;
                }
            }
            else //currently moving along negative quadrants
            {
                if (yaxis) //moving down yaxis
                {
                    if (turn == 'R')
                        increaseCoord = false;
                    else if (turn == 'L')
                        increaseCoord = true;
                }
                else
                {
                    if (turn == 'R')
                        increaseCoord = true;
                    else if (turn == 'L')
                        increaseCoord = false;
                }
            }

            return (increaseCoord);
        }

        #endregion
    }
}