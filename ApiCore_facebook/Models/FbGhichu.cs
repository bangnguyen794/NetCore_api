using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbGhichu
    {
        public int Id { get; set; }
        public string Noidung { get; set; }
        public DateTime? Time { get; set; }
        public string NameFc { get; set; }
        public string IdUser { get; set; }
        public string IdPage { get; set; }
    }
}
