using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbGuitinHangloat
    {
        public int Id { get; set; }
        public DateTime? NgayBdgui { get; set; }
        public int? KhoangNgaygui { get; set; }
        public string DanhsachPage { get; set; }
        public bool? KhKhongphanhoi { get; set; }
        public bool? KhCotuongtac { get; set; }
        public bool? KhTuongtactot { get; set; }
        public bool? KhDamuahang { get; set; }
        public bool? TtTuvan { get; set; }
        public bool? TtKhongmuahang { get; set; }
        public bool? TtDamuahang { get; set; }
        public bool? Hengio { get; set; }
        public string Noidung { get; set; }
        public DateTime? ThoigianHengui { get; set; }
    }
}
