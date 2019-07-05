using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbDonviVanchuyenChitiet
    {
        public int Id { get; set; }
        public string AppId { get; set; }
        public string IdPage { get; set; }
        public int? IdDonviVanchuyen { get; set; }
        public string Nguoitao { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string Link { get; set; }
        public string ApiKey { get; set; }
        public string Weight { get; set; }
        public string MakhoViettel { get; set; }
        public string CusidViettel { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? Trangthai { get; set; }
        public string Nguoisua { get; set; }
        public DateTime? Ngaysua { get; set; }
        public string TenNguoilienhe { get; set; }
        public string DiachiLayhang { get; set; }
        public string SodienthoaiNguoilienhe { get; set; }
        public string Tinhthanh { get; set; }
        public string Quanhuyen { get; set; }
    }
}
