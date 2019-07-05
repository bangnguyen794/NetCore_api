using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbLichhen
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public string Ghichu { get; set; }
        public string IdMessage { get; set; }
        public DateTime? Ngayhen { get; set; }
        public string Nguoitao { get; set; }
        public bool? Xong { get; set; }
    }
}
