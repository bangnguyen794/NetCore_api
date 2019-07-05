using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbSanpham
    {
        public int Id { get; set; }
        public string Hinhanh { get; set; }
        public string Tensp { get; set; }
        public string Mota { get; set; }
        public string Gia { get; set; }
        public string IdPage { get; set; }
        public int? WebConnect { get; set; }
        public DateTime? Ngaytao { get; set; }
        public DateTime? Ngaysua { get; set; }
        public string Nguoitao { get; set; }
        public string Khoiluong { get; set; }
        public bool? Thungrac { get; set; }
        public bool? Trangthai { get; set; }
    }
}
