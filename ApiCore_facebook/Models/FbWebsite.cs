using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbWebsite
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Link { get; set; }
        public string ApiKey { get; set; }
        public bool? Trangthai { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string Nguoitao { get; set; }
        public bool? SenMailThongke { get; set; }
        public string UrlImg { get; set; }
        public int? GioihanGui { get; set; }
        public bool? NoPaste { get; set; }
        public bool? KhoaGhichu { get; set; }
    }
}
