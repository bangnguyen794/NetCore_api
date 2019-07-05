using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbThongbaoVanchuyen
    {
        public int Id { get; set; }
        public string Mahd { get; set; }
        public string Magiaohang { get; set; }
        public string Account { get; set; }
        public int? TrangthaiGiaohang { get; set; }
        public string ChitietDon { get; set; }
        public string Link { get; set; }
        public string IdPage { get; set; }
        public string AccountNhanvien { get; set; }
        public DateTime? Thoigiancapnhap { get; set; }
    }
}
