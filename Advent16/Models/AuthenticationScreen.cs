using Advent16.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Advent16.Models
{
    public class AuthenticationScreen
    {
        public bool[,] Pixels { get; set; }

        public int LitPixelCount
        {
            get
            {
                return Pixels.Cast<bool>().Where(x => x == true).Count();
            }
        }

        public AuthenticationScreen()
        {
            Pixels = new bool[6, 50]; //The screen is 50 pixels wide and 6 pixels tall, all of which start off
        }

        public void UpdateScreen(InstructionStep step)
        {
            switch (step.Operation)
            {
                case ScreenOperations.rect:
                    TurnOnSubRectangle(step.RecWidth ?? 0, step.RecHeight ?? 0);
                    break;
                case ScreenOperations.rotatecolumn:
                    for (int i = 1; i <= step.MoveNum; i++)
                    {
                        ShiftDown(step.ArrayNum ?? 0);
                    }
                    break;
                case ScreenOperations.rotaterow:
                    for (int i = 1; i <= step.MoveNum; i++)
                    {
                        ShiftRight(step.ArrayNum ?? 0);
                    }
                    break;
            }
        }

        public void ShiftDown(int column)
        {
            int count = Pixels.GetLength(0) - 1;
            bool lastval = Pixels[count, column];
            for (int i = count; i >= 1; i--) //loop through each y val in column indicated
            {
                Pixels[i, column] = Pixels[i - 1, column]; //set each y val for that x position to the value of the 
            }
            Pixels[0, column] = lastval;
            
        }

        public void ShiftRight(int row)
        {
            int count = Pixels.GetLength(1) - 1;
            bool lastval = Pixels[row, count];
            for (int i = count; i >= 1; i--) //loop through each x val in column indicated
            {
                Pixels[row, i] = Pixels[row, i - 1]; //set each y val for that x position to the value of the 
            }
            Pixels[row, 0] = lastval;
        }

        public void TurnOnSubRectangle(int xval, int yval)
        {
            for (int x = 0; x <= yval - 1; x++) //update these - had labeled incorrectly
            {
                for (int y = 0; y <= xval - 1; y++)
                {
                    Pixels[x, y] = true;
                }
            }
        }
    }

    public class EncodedInstructions
    {
        public string Input { get; set; }

        public List<InstructionStep> StepsFromInput
        {
            get
            {
                List<string> textSteps = Input.Split('&').ToList();

                List<InstructionStep> composedSteps = new List<InstructionStep>();

                foreach (string step in textSteps)
                {
                    InstructionStep thisStep = new InstructionStep();
                    if (step.Contains("rect"))
                    {
                        thisStep.Operation = ScreenOperations.rect;
                        string dimensions = step.Split(' ')[1];
                        thisStep.RecWidth = int.Parse(dimensions.Split('x')[0]);
                        thisStep.RecHeight = int.Parse(dimensions.Split('x')[1]);
                    }
                    else if (step.Contains("rotate"))
                    {
                        if (step.Contains("row"))
                        {
                            thisStep.Operation = ScreenOperations.rotaterow;
                        }
                        else if (step.Contains("column"))
                        {
                            thisStep.Operation = ScreenOperations.rotatecolumn;
                        }
                        string ystring = step.Split(' ')[2];
                        thisStep.ArrayNum = int.Parse(ystring.Split('=')[1]);
                        thisStep.MoveNum = int.Parse(step.Split(' ')[4]);
                    }
                    composedSteps.Add(thisStep);
                }
                return composedSteps;
            }
        }

        public EncodedInstructions()
        {
            Input = "rect 1x1&rotate row y=0 by 5&rect 1x1&rotate row y=0 by 5&rect 1x1&rotate row y=0 by 3&rect 1x1&rotate row y=0 by 2&rect 1x1&rotate row y=0 by 3&rect 1x1&rotate row y=0 by 2&rect 1x1&rotate row y=0 by 5&rect 1x1&rotate row y=0 by 5&rect 1x1&rotate row y=0 by 3&rect 1x1&rotate row y=0 by 2&rect 1x1&rotate row y=0 by 3&rect 2x1&rotate row y=0 by 2&rect 1x2&rotate row y=1 by 5&rotate row y=0 by 3&rect 1x2&rotate column x=30 by 1&rotate column x=25 by 1&rotate column x=10 by 1&rotate row y=1 by 5&rotate row y=0 by 2&rect 1x2&rotate row y=0 by 5&rotate column x=0 by 1&rect 4x1&rotate row y=2 by 18&rotate row y=0 by 5&rotate column x=0 by 1&rect 3x1&rotate row y=2 by 12&rotate row y=0 by 5&rotate column x=0 by 1&rect 4x1&rotate column x=20 by 1&rotate row y=2 by 5&rotate row y=0 by 5&rotate column x=0 by 1&rect 4x1&rotate row y=2 by 15&rotate row y=0 by 15&rotate column x=10 by 1&rotate column x=5 by 1&rotate column x=0 by 1&rect 14x1&rotate column x=37 by 1&rotate column x=23 by 1&rotate column x=7 by 2&rotate row y=3 by 20&rotate row y=0 by 5&rotate column x=0 by 1&rect 4x1&rotate row y=3 by 5&rotate row y=2 by 2&rotate row y=1 by 4&rotate row y=0 by 4&rect 1x4&rotate column x=35 by 3&rotate column x=18 by 3&rotate column x=13 by 3&rotate row y=3 by 5&rotate row y=2 by 3&rotate row y=1 by 1&rotate row y=0 by 1&rect 1x5&rotate row y=4 by 20&rotate row y=3 by 10&rotate row y=2 by 13&rotate row y=0 by 10&rotate column x=5 by 1&rotate column x=3 by 3&rotate column x=2 by 1&rotate column x=1 by 1&rotate column x=0 by 1&rect 9x1&rotate row y=4 by 10&rotate row y=3 by 10&rotate row y=1 by 10&rotate row y=0 by 10&rotate column x=7 by 2&rotate column x=5 by 1&rotate column x=2 by 1&rotate column x=1 by 1&rotate column x=0 by 1&rect 9x1&rotate row y=4 by 20&rotate row y=3 by 12&rotate row y=1 by 15&rotate row y=0 by 10&rotate column x=8 by 2&rotate column x=7 by 1&rotate column x=6 by 2&rotate column x=5 by 1&rotate column x=3 by 1&rotate column x=2 by 1&rotate column x=1 by 1&rotate column x=0 by 1&rect 9x1&rotate column x=46 by 2&rotate column x=43 by 2&rotate column x=24 by 2&rotate column x=14 by 3&rotate row y=5 by 15&rotate row y=4 by 10&rotate row y=3 by 3&rotate row y=2 by 37&rotate row y=1 by 10&rotate row y=0 by 5&rotate column x=0 by 3&rect 3x3&rotate row y=5 by 15&rotate row y=3 by 10&rotate row y=2 by 10&rotate row y=0 by 10&rotate column x=7 by 3&rotate column x=6 by 3&rotate column x=5 by 1&rotate column x=3 by 1&rotate column x=2 by 1&rotate column x=1 by 1&rotate column x=0 by 1&rect 9x1&rotate column x=19 by 1&rotate column x=10 by 3&rotate column x=5 by 4&rotate row y=5 by 5&rotate row y=4 by 5&rotate row y=3 by 40&rotate row y=2 by 35&rotate row y=1 by 15&rotate row y=0 by 30&rotate column x=48 by 4&rotate column x=47 by 3&rotate column x=46 by 3&rotate column x=45 by 1&rotate column x=43 by 1&rotate column x=42 by 5&rotate column x=41 by 5&rotate column x=40 by 1&rotate column x=33 by 2&rotate column x=32 by 3&rotate column x=31 by 2&rotate column x=28 by 1&rotate column x=27 by 5&rotate column x=26 by 5&rotate column x=25 by 1&rotate column x=23 by 5&rotate column x=22 by 5&rotate column x=21 by 5&rotate column x=18 by 5&rotate column x=17 by 5&rotate column x=16 by 5&rotate column x=13 by 5&rotate column x=12 by 5&rotate column x=11 by 5&rotate column x=3 by 1&rotate column x=2 by 5&rotate column x=1 by 5&rotate column x=0 by 1";
        }
    }

    public class InstructionStep
    {
        public ScreenOperations Operation { get; set; }

        public int? ArrayNum { get; set; } //for rotate, x or y grouping the screen operation affects

        public int? MoveNum { get; set; } //for rotate, spaces to shift

        public int? RecHeight { get; set; } //if Operation is rect, this is the x val of the new rectangle

        public int? RecWidth { get; set; } //if Operation is rect, this is the y val of the new rectangle
    }

    public enum ScreenOperations
    {
        rect,
        rotaterow,
        rotatecolumn
    }

    public class FinalDisplay
    {
        public int LitPixelCount
        {
            get
            {
                return ScreenPixels.Cast<bool>().Where(x => x == true).Count();
            }
        }

        public List<string> ScreenDisplay
        {
            get
            {
                List<string> screenLines = new List<string>();
                for (int row = 0; row < ScreenPixels.GetLength(0); row++)
                {
                    string lineText = "";
                    for (int col = 0; col < ScreenPixels.GetLength(1); col++)
                    {
                        if (ScreenPixels[row,col])
                        {
                            lineText += '%'; //on pixel
                        }
                        else
                        {
                            lineText += '0'; //off pixel
                        }
                    }
                    screenLines.Add(lineText);
                }
                return screenLines;
            }
        }

        public bool[,] ScreenPixels { get; set; }

        public FinalDisplay(bool[,] screen)
        {
            ScreenPixels = screen;
        }
    }
}