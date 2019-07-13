using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.Library
{
    public class NewUserToken
    {
        public int Id { get; set; }
        public string AppId { get; set; }
        public string IdUser { get; set; }
        public string NameUser { get; set; }
        public string AccessToken { get; set; }
        public string Perms { get; set; }
       
    }
}
