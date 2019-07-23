using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbActiveUser
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public string IdUser { get; set; }
        public DateTime? ActiveTime { get; set; }
        public string Goicuoc { get; set; }
        public bool? Acctive { get; set; }
    }
}
