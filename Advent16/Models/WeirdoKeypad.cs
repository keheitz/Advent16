using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Advent16.Models
{
    public class WeirdoKeypad
    {
        public char?[,] Buttons { get; set; }

        public List<char> ButtonsTraversed { get; set; }

        public void Move(IMoveWeird imover)
        {
            imover.Move(this, ButtonsTraversed.Last());
        }
        
        public WeirdoKeypad()
        {
            Buttons = new char?[5, 5] { { null,null,'1',null,null}, { null, '2', '3', '4', null }, { '5', '6', '7', '8', '9' }, { null, 'A', 'B', 'C', null }, { null, null, 'D', null, null } };

            ButtonsTraversed = new List<char>();
        }
    }
    

    public class WeirdoBathroomCode
    {
        public List<char> Code { get; set; }

        public WeirdoBathroomCode()
        {
            Code = new List<char>();
        }
    }
    
    public interface IMoveWeird
    {
        char MoveInstruction { get; }
        void Move(WeirdoKeypad keypad, char startbutton);
    }

    public class UpMoverWeird : IMoveWeird
    {

        public void Move(WeirdoKeypad keypad, char startbutton)
        {
            Tuple<int, int> start = Library.Common.CoordinatesOf(keypad.Buttons, startbutton);
            if(start.Item1 > 0)
            {
                char? nextval = keypad.Buttons[start.Item1 - 1, start.Item2];
                if(nextval != null)
                {
                    char button = nextval ?? 'Z'; //will never be null
                    keypad.ButtonsTraversed.Add(button);
                }
            }
        }

        public char MoveInstruction
        {
            get { return 'U'; }
        }
    }

    public class DownMoverWeird : IMoveWeird
    {
        public void Move(WeirdoKeypad keypad, char startbutton)
        {
            Tuple<int, int> start = Library.Common.CoordinatesOf(keypad.Buttons, startbutton);
            if (start.Item1 < 4)
            {
                char? nextval = keypad.Buttons[start.Item1 + 1, start.Item2];
                if (nextval != null)
                {
                    char button = nextval ?? 'Z'; //will never be null
                    keypad.ButtonsTraversed.Add(button);
                }
            }
        }

        public char MoveInstruction
        {
            get { return 'D'; }
        }
    }

    public class RightMoverWeird : IMoveWeird
    {

        public void Move(WeirdoKeypad keypad, char startbutton)
        {
            Tuple<int, int> start = Library.Common.CoordinatesOf(keypad.Buttons, startbutton);
            if (start.Item2 < 4)
            {
                char? nextval = keypad.Buttons[start.Item1, start.Item2 + 1];
                if (nextval != null)
                {
                    char button = nextval ?? 'Z'; //will never be null
                    keypad.ButtonsTraversed.Add(button);
                }
            }
        }
        
        public char MoveInstruction
        {
            get { return 'R'; }
        }
    }

    public class LeftMoverWeird : IMoveWeird
    {
        public void Move(WeirdoKeypad keypad, char startbutton)
        {
            Tuple<int, int> start = Library.Common.CoordinatesOf(keypad.Buttons, startbutton);
            if (start.Item2 > 0)
            {
                char? nextval = keypad.Buttons[start.Item1, start.Item2 - 1];
                if (nextval != null)
                {
                    char button = nextval ?? 'Z'; //will never be null
                    keypad.ButtonsTraversed.Add(button);
                }
            }
        }

        public char MoveInstruction
        {
            get { return 'L'; }
        }
    }

    public static class WeirdMoverFactory
    {
        private static List<IMoveWeird> movers = new List<IMoveWeird>();

        public static IMoveWeird GetMover(char movechar)
        {
            if (movers.Count == 0)
            {
                movers = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(type => typeof(IMoveWeird).IsAssignableFrom(type) && type.IsClass)
                                   .Select(type => Activator.CreateInstance(type))
                                   .Cast<IMoveWeird>()
                                   .ToList();
            }

            return movers.Where(m => m.MoveInstruction == movechar).FirstOrDefault();
        }
    }

}