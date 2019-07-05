using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbBaoxau
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string IdPage { get; set; }
        public string Noidung { get; set; }
        public string Nguoitao { get; set; }
        public DateTime? Ngaytao { get; set; }
    }
}
