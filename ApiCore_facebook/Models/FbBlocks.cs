using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbBlocks
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string IdPage { get; set; }
        public string Nguoichan { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string IdMessage { get; set; }
    }
}
