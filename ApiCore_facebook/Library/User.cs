using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.Library
{
    public class User
    {
        public int Id { get; set; }
        public string id_user { get; set; }
       
        public string Fullname { get; set; }
      
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
