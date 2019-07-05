using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbGoicuocChitiet
    {
        public int Id { get; set; }
        public string MaGoicuoc { get; set; }
        public int? Sotrang { get; set; }
        public int? Sonhanvien { get; set; }
        public int? Sothang { get; set; }
        public DateTime? NgayKichhoat { get; set; }
        public DateTime? NgayKetthuc { get; set; }
        public string Sotien { get; set; }
        public string IdUser { get; set; }
        public string AppId { get; set; }
        public bool? Status { get; set; }
        public DateTime? Ngaytao { get; set; }
        public int? GioihanGui { get; set; }
        public bool? SenMailThongke { get; set; }
        public string Nguoikichhoat { get; set; }
        public bool? Nangcap { get; set; }
        public string TienNangcap { get; set; }
        public DateTime? ThoigianNangcap { get; set; }
        public string Nguoinangcap { get; set; }
        public int? SotrangNc { get; set; }
        public int? SonhanvienNc { get; set; }
        public int? SothangNc { get; set; }
        public string UserActive { get; set; }
        public DateTime? ThoigianCapnhap { get; set; }
        public string Nguoicapnhap { get; set; }
        public bool? Mienphi { get; set; }
    }
}
