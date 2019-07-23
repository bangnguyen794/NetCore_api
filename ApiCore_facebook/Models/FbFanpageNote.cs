using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbFanpageNote
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public string Contents { get; set; }
        public bool? Tick { get; set; }
        public int? Sort { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string Shortcut { get; set; }
        public string NameFace { get; set; }
        public bool? Status { get; set; }
    }
}
