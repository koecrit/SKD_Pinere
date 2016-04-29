using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinere.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string LongName { get; set; }
        public string Email { get; set; }
        public string Responsible { get; set; }
    }
}