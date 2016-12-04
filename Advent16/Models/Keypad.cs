using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Advent16.Models
{
    public class Keypad
    {
        public int[,] Buttons { get; set; }

        public List<int> ButtonsTraversed { get; set; }

        public void Move(IMove imover)
        {
            imover.Move(this, ButtonsTraversed.Last());
        }
        
        public Keypad()
        {
            Buttons = new int[3, 3] { { 1, 2, 3}, { 4, 5, 6}, { 7, 8, 9} };

            ButtonsTraversed = new List<int>();
        }
    }

    public class CodeInstructions
    {
        public string Instructions { get; set; }

        public CodeInstructions()
        {
            string text = "LRRLLLRDRURUDLRDDURULRULLDLRRLRLDULUDDDDLLRRLDUUDULDRURRLDULRRULDLRDUDLRLLLULDUURRRRURURULURRULRURDLULURDRDURDRLRRUUDRULLLLLDRULDDLLRDLURRLDUURDLRLUDLDUDLURLRLDRLUDUULRRRUUULLRDURUDRUDRDRLLDLDDDLDLRRULDUUDULRUDDRLLURDDRLDDUDLLLLULRDDUDDUUULRULUULRLLDULUDLLLLURRLDLUDLDDLDRLRRDRDUDDDLLLLLRRLLRLUDLULLDLDDRRUDDRLRDDURRDULLLURLRDLRRLRDLDURLDDULLLDRRURDULUDUDLLLDDDLLRLDDDLLRRLLURUULULDDDUDULUUURRUUDLDULULDRDDLURURDLDLULDUDUDDDDD%RUURUDRDUULRDDLRLLLULLDDUDRDURDLRUULLLLUDUDRRUDUULRRUUDDURDDDLLLLRRUURULULLUDDLRDUDULRURRDRDLDLDUULUULUDDLUDRLULRUDRDDDLRRUUDRRLULUULDULDDLRLURDRLURRRRULDDRLDLLLRULLDURRLUDULDRDUDRLRLULRURDDRLUDLRURDDRDULUDLDLLLDRLRUDLLLLLDUDRDUURUDDUDLDLDUDLLDLRRDLULLURLDDUDDRDUDLDDUULDRLURRDLDLLUUDLDLURRLDRDDLLDLRLULUDRDLLLDRLRLLLDRUULUDLLURDLLUURUDURDDRDRDDUDDRRLLUULRRDRULRURRULLDDDUDULDDRULRLDURLUDULDLDDDLRULLULULUDLDDRDLRDRDLDULRRLRLRLLLLLDDDRDDULRDULRRLDLUDDDDLUDRLLDLURDLRDLDRDRDURRDUDULLLDLUDLDRLRRDDDRRLRLLULDRLRLLLLDUUURDLLULLUDDRLULRDLDLDURRRUURDUDRDLLLLLLDDDURLDULDRLLDUDRULRRDLDUDRLLUUUDULURRUR%URRRLRLLDDDRRLDLDLUDRDRDLDUDDDLDRRDRLDULRRDRRDUDRRUUDUUUDLLUURLRDRRURRRRUDRLLLLRRDULRDDRUDLRLUDURRLRLDDRRLUULURLURURUDRULDUUDLULUURRRDDLRDLUDRDLDDDLRUDURRLLRDDRDRLRLLRLRUUDRRLDLUDRURUULDUURDRUULDLLDRDLRDUUDLRLRRLUDRRUULRDDRDLDDULRRRURLRDDRLLLRDRLURDLDRUULDRRRLURURUUUULULRURULRLDDDDLULRLRULDUDDULRUULRRRRRLRLRUDDURLDRRDDULLUULLDLUDDDUURLRRLDULUUDDULDDUULLLRUDLLLRDDDLUUURLDUDRLLLDRRLDDLUDLLDLRRRLDDRUULULUURDDLUR%UULDRLUULURDRLDULURLUDULDRRDULULUDLLDURRRURDRLRLLRLDDLURRDLUUDLULRDULDRDLULULULDDLURULLULUDDRRULULULRDULRUURRRUDLRLURDRURDRRUDLDDUURDUUDLULDUDDLUUURURLRRDLULURDURRRURURDUURDRRURRDDULRULRRDRRDRUUUUULRLUUUDUUULLRRDRDULRDDULDRRULRLDLLULUUULUUDRDUUUDLLULDDRRDULUURRDUULLUUDRLLDUDLLLURURLUDDLRURRDRLDDURLDLLUURLDUURULLLRURURLULLLUURUUULLDLRDLUDDRRDDUUDLRURDDDRURUURURRRDLUDRLUULDUDLRUUDRLDRRDLDLDLRUDDDDRRDLDDDLLDLULLRUDDUDDDLDDUURLDUDLRDRURULDULULUDRRDLLRURDULDDRRDLUURUUULULRURDUUDLULLURUDDRLDDUDURRDURRUURLDLLDDUUDLLUURDRULLRRUUURRLLDRRDLURRURDULDDDDRDD%LLRUDRUUDUDLRDRDRRLRDRRUDRDURURRLDDDDLRDURDLRRUDRLLRDDUULRULURRRLRULDUURLRURLRLDUDLLDULULDUUURLRURUDDDDRDDLLURDLDRRUDRLDULLRULULLRURRLLURDLLLRRRRDRULRUDUDUDULUURUUURDDLDRDRUUURLDRULDUDULRLRLULLDURRRRURRRDRULULUDLULDDRLRRULLDURUDDUULRUUURDRRLULRRDLDUDURUUUUUURRUUULURDUUDLLUURDLULUDDLUUULLDURLDRRDDLRRRDRLLDRRLUDRLLLDRUULDUDRDDRDRRRLUDUDRRRLDRLRURDLRULRDUUDRRLLRLUUUUURRURLURDRRUURDRRLULUDULRLLURDLLULDDDLRDULLLUDRLURDDLRURLLRDRDULULDDRDDLDDRUUURDUUUDURRLRDUDLRRLRRRDUULDRDUDRLDLRULDL";
            text = text.Replace("%", System.Environment.NewLine);
            Instructions = text;
        }
    }

    public class BathroomCode
    {
        public List<int> Code { get; set; }

        public BathroomCode()
        {
            Code = new List<int>();
        }
    }
    
    public interface IMove
    {
        char MoveInstruction { get; }
        void Move(Keypad keypad, int startbutton);
    }

    public class UpMover : IMove
    {

        public void Move(Keypad keypad, int startbutton)
        {
            Tuple<int, int> start = Library.Common.CoordinatesOf(keypad.Buttons, startbutton);
            if(start.Item1 > 0)
            {
                int button = keypad.Buttons[start.Item1 - 1, start.Item2];
                keypad.ButtonsTraversed.Add(button);
            }
        }

        public char MoveInstruction
        {
            get { return 'U'; }
        }
    }

    public class DownMover : IMove
    {

        public void Move(Keypad keypad, int startbutton)
        {
            Tuple<int, int> start = Library.Common.CoordinatesOf(keypad.Buttons, startbutton);
            if (start.Item1 < 2)
            {
                int button = keypad.Buttons[start.Item1 + 1, start.Item2];
                keypad.ButtonsTraversed.Add(button);
            }
        }

        public char MoveInstruction
        {
            get { return 'D'; }
        }
    }

    public class RightMover : IMove
    {

        public void Move(Keypad keypad, int startbutton)
        {
            Tuple<int, int> start = Library.Common.CoordinatesOf(keypad.Buttons, startbutton);
            if (start.Item2 < 2)
            {
                int button = keypad.Buttons[start.Item1, start.Item2 + 1];
                keypad.ButtonsTraversed.Add(button);
            }
        }

        public char MoveInstruction
        {
            get { return 'R'; }
        }
    }

    public class LeftMover : IMove
    {

        public void Move(Keypad keypad, int startbutton)
        {
            Tuple<int, int> start = Library.Common.CoordinatesOf(keypad.Buttons, startbutton);
            if (start.Item2 > 0)
            {
                int button = keypad.Buttons[start.Item1, start.Item2 - 1];
                keypad.ButtonsTraversed.Add(button);
            }
        }

        public char MoveInstruction
        {
            get { return 'L'; }
        }
    }

    public static class MoverFactory
    {
        private static List<IMove> movers = new List<IMove>();

        public static IMove GetMover(char movechar)
        {
            if (movers.Count == 0)
            {
                movers = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(type => typeof(IMove).IsAssignableFrom(type) && type.IsClass)
                                   .Select(type => Activator.CreateInstance(type))
                                   .Cast<IMove>()
                                   .ToList();
            }

            return movers.Where(m => m.MoveInstruction == movechar).FirstOrDefault();
        }
    }

}