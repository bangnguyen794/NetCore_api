using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbLichsuxem
    {
        public int Id { get; set; }
        public string Xemboi { get; set; }
        public string Account { get; set; }
        public string IdMessage { get; set; }
        public string IdPage { get; set; }
        public DateTime? Datetime { get; set; }
    }
}
