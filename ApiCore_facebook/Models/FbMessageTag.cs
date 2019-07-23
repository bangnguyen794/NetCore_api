using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbMessageTag
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public int? IdTag { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string IdMessage { get; set; }
    }
}
