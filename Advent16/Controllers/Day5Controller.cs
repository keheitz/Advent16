using Advent16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Advent16.Controllers
{
    public class Day5Controller : Controller
    {
        // GET: Day6
        public ActionResult Index()
        {
            DoorInput input = new DoorInput();
            return View(input);
        }

        public ActionResult GetPassword(DoorInput input)
        {
            int index = 0;
            StringBuilder password = new StringBuilder();
            while(password.Length < 8)
            {
                string hash = CalculateMD5Hash(input.DoorID + index.ToString());
                if (hash.Substring(0,5) == "00000")
                {
                    password.Append(hash[5]);
                }
                index++;
            }
            DoorPassword pass = new DoorPassword();
            pass.Password = password.ToString();
            return PartialView("Password", pass);
        }

        public ActionResult GetSecondPassword(DoorInput input)
        {
            int index = 0;
            string startstring = new String('-', 8);
            char[] password = startstring.ToCharArray();
            while (password.Any(x => x == '-'))
            {
                string hash = CalculateMD5Hash(input.DoorID + index.ToString());
                if (hash.Substring(0, 5) == "00000")
                {
                    int hashIndex;
                    if(Int32.TryParse(hash[5].ToString(), out hashIndex))
                    {
                        if(hashIndex < 8 && password[hashIndex] == '-')
                        {
                            password[hashIndex] = hash[6];
                        }
                    }
                }
                index++;
            }
            DoorPassword pass = new DoorPassword();
            pass.Password = new String(password);
            return PartialView("Password", pass);
        }


        //https://blogs.msdn.microsoft.com/csharpfaq/2006/10/09/how-do-i-calculate-a-md5-hash-from-a-string/
        private string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}