using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbTinQc
    {
        public string BroadcastId { get; set; }
        public string IdPage { get; set; }
        public string MessageCreativeId { get; set; }
        public string CustomLabelId { get; set; }
        public string ContentMsCreative { get; set; }
        public DateTime? DateTime { get; set; }
        public string Status { get; set; }
        public string CountTin { get; set; }
        public string Type { get; set; }
    }
}
