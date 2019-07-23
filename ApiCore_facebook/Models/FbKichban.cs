using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbKichban
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Noidung { get; set; }
        public string Trang { get; set; }
        public string Nhom { get; set; }
        public bool? Trangthai { get; set; }
        public string Nguoitao { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string ListKichban { get; set; }
        public string NguoiCapnhap { get; set; }
        public int? WebConnect { get; set; }
        public string IdNguoitao { get; set; }
    }
}
