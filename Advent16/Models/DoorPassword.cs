using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advent16.Models
{
    public class DoorPassword
    {
        public string Password { get; set; }
        
    }

    public class DoorInput
    {
        public string DoorID { get; set; }

        public DoorInput()
        {
            DoorID = "abbhdwsy";
        }
    }
}