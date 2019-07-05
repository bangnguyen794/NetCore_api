using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbGoicuocFanpage
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public string MaGoicuoc { get; set; }
        public string Nhanvien { get; set; }
        public bool? Active { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string Nguoitao { get; set; }
        public DateTime? Ngaycapnhap { get; set; }
    }
}
