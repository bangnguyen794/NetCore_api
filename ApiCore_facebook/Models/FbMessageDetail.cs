using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbMessageDetail
    {
        public int I { get; set; }
        public string Id { get; set; }
        public string IdMessage { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string SenderIdBy { get; set; }
        public string SenderNameBy { get; set; }
        public string Category { get; set; }
    }
}
