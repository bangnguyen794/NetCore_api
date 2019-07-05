using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbThongbao
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string Tieude { get; set; }
        public string Noidung { get; set; }
        public DateTime? Thoigian { get; set; }
        public string Nguoitao { get; set; }
        public string Nguoidoc { get; set; }
        public DateTime? Ngaytao { get; set; }
        public bool? Trangthai { get; set; }
        public string Link { get; set; }
        public bool? Button { get; set; }
        public string Style { get; set; }
        public bool? Thungrac { get; set; }
        public bool? Gimdau { get; set; }
    }
}
