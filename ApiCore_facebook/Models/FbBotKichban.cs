using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbBotKichban
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public string Noidung { get; set; }
        public bool? Trangthai { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
