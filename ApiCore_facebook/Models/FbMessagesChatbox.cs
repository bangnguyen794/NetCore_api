using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbMessagesChatbox
    {
        public int I { get; set; }
        public string Id { get; set; }
        public string IdPage { get; set; }
        public string IdUser { get; set; }
        public string Message { get; set; }
        public string NameUser { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? AutoBot { get; set; }
        public int? IdKichban { get; set; }
    }
}
