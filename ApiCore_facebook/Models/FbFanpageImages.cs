using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbFanpageImages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdPage { get; set; }
        public bool? Tick { get; set; }
        public int? Sort { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedIdBy { get; set; }
        public bool? Status { get; set; }
        public int? WebConnect { get; set; }
    }
}
