using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class TinhthanhQuanhuyen
    {
        public int Id { get; set; }
        public string Thanhpho { get; set; }
        public string Quanhuyen { get; set; }
        public string MaThanhpho { get; set; }
        public string MaQuanhuyen { get; set; }
    }
}
