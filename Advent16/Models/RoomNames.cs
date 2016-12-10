using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Advent16.Models
{
    public class RoomName
    {
        public int SectorID { get; set; }

        public string Checksum { get; set; }

        public string MostCommonLetters
        {
            get
            {
                List<LetterCount> letterCounts = EncryptedRoomName.Replace("-", "").GroupBy(x => x)
                       .Select(x => new LetterCount
                       {
                           Letter = x.Key,
                           Count = x.Count()
                       }).ToList();

                List<char> topLetters = letterCounts.OrderByDescending(x => x.Count).ThenBy(x => x.Letter).Take(5).Select(x => x.Letter).ToList();
                string expectedChecksum = "";
                foreach(char letter in topLetters)
                {
                    expectedChecksum += letter;
                }

                return expectedChecksum;
            }
        }

        public string EncryptedRoomName { get; set; }
        
        public string DecryptedRoomName
        {
            get
            {
                /*
                 * To decrypt a room name, rotate each letter forward through the alphabet 
                 * a number of times equal to the room's sector ID. 
                 * A becomes B, B becomes C, Z becomes A, and so on. Dashes become spaces.
                 */
                string name = "";
                char[] lettersToReplace = EncryptedRoomName.Replace("-", " ").ToCharArray();
                Decoder decoder = new Decoder();
                foreach(char letter in lettersToReplace)
                {
                    if(letter != ' ')
                    {
                        char replaceLetter = decoder.FindDecodedLetter(letter, SectorID);
                        name += replaceLetter.ToString();
                    }
                    else
                    {
                        name += " ";
                    }
                }
                
                return name;
            }
        }

        public bool DecoyRoom
        {
            get
            {
                bool decoy = true;
                if(MostCommonLetters == Checksum)
                {
                    decoy = false;
                }

                return decoy;
            }
        }

        public RoomName(string room)
        {
            Checksum = room.Split('[', ']')[1];
            
            SectorID = Int32.Parse(Regex.Match(room, @"\d+").Value); //regex to pull numbers - all integers in string belong to sector id

            EncryptedRoomName = room.Substring(0, room.LastIndexOf("-"));
        }
    }

    public class RoomListDocument
    {
        [Display(Name="Solution Count")]
        public bool Part1 { get; set; }

        [Display(Name ="List of Rooms")]
        [Required]
        public HttpPostedFileBase RoomList { get; set; }
    }

    public class SectorIDSum
    {
        public int Total { get; set; }
    }

    public class LetterCount
    {
        public char Letter { get; set; }

        public int Count { get; set; }
    }

    public class Decoder
    {
        char[]  Alpha
        {
            get
            {
                char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray(); //26
                return letters;
            }
        }

        public char FindDecodedLetter(char letter, int sector)
        {
            char decodedLetter;
            int moves = sector % 26; //we would loop every 26, only care about moves that don't take us back to start
            int ixLetter = Array.IndexOf(Alpha, letter);
            if ((ixLetter + moves) > 25) //need to move from start
            {
                int skip = 25 - ixLetter;
                int remaining = moves - skip;
                decodedLetter = Alpha[remaining - 1];
            }
            else
            {
                decodedLetter = Alpha[(ixLetter + moves)];
            }
            return decodedLetter; 
        }
            
    }
}