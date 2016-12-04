using Advent16.Library;
using Advent16.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Advent16.Controllers
{
    /* PART ONE
     * You arrive at Easter Bunny Headquarters under cover of darkness. However, you left in such a rush that you forgot to use the bathroom! 
     * Fancy office buildings like this one usually have keypad locks on their bathrooms, so you search the front desk for the code.
     * "In order to improve security," the document you find says, "bathroom codes will no longer be written down. 
     * Instead, please memorize and follow the procedure below to access the bathrooms."
     * 
     * The document goes on to explain that each button to be pressed can be found by starting on the previous button and moving to adjacent buttons on the keypad: 
     * U moves up, D moves down, L moves left, and R moves right. Each line of instructions corresponds to one button, starting at the previous button 
     * (or, for the first line, the "5" button); press whatever button you're on at the end of each line. 
     * If a move doesn't lead to a button, ignore it.
     */

    /* PART TWO
     * You finally arrive at the bathroom and go to punch in the code. 
     * Much to your bladder's dismay, the keypad is not at all like you imagined it. 
     * Instead, you are confronted with the result of hundreds of man-hours of bathroom-keypad-design meetings
     */

    public class Day2Controller : Controller
    {
        // GET: Day1 - Main view for entering and parsing keypad code directions
        public ActionResult Index()
        {
            CodeInstructions procedure = new CodeInstructions();
            return View(procedure);
        }

        //TODO:I think I could use same models for both, if I pass in keypad layout and use chars (as in WeirdoKeypad)
        //refactor so there is less logical duplication - but right now I'm behind on puzzles!

        [HttpGet]
        public ActionResult GetCode(CodeInstructions procedure)
        {
            BathroomCode input = new BathroomCode();
            var reader = new StringReader(procedure.Instructions);
            string line;
            Keypad keypad = new Keypad();
            keypad.ButtonsTraversed.Add(5); //start at 5, middle of keypad
            while (null != (line = reader.ReadLine()))
            {
                char[] array = line.ToCharArray();

                // Loop through array.
                for (int i = 0; i < array.Length; i++)
                {
                    // Get character from array.
                    char letter = array[i];
                    IMove mover = MoverFactory.GetMover(letter);
                    keypad.Move(mover);
                }
                input.Code.Add(keypad.ButtonsTraversed.Last());
            }
            return PartialView("Code", input);
        }

        public ActionResult GetWeirdCode(CodeInstructions procedure)
        {
            WeirdoBathroomCode input = new WeirdoBathroomCode();
            var reader = new StringReader(procedure.Instructions);
            string line;
            WeirdoKeypad keypad = new WeirdoKeypad();
            keypad.ButtonsTraversed.Add('5'); //still start at 5
            while (null != (line = reader.ReadLine()))
            {
                char[] array = line.ToCharArray();

                // Loop through array.
                for (int i = 0; i < array.Length; i++)
                {
                    // Get character from array.
                    char letter = array[i];
                    IMoveWeird mover = WeirdMoverFactory.GetMover(letter);
                    keypad.Move(mover);
                }
                input.Code.Add(keypad.ButtonsTraversed.Last());
            }
            return PartialView("WeirdoCode", input);
        }

    }
}
 