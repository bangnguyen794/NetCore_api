using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbHengui
    {
        public int Id { get; set; }
        public string IdPage { get; set; }
        public string IdUser { get; set; }
        public string IdMessage { get; set; }
        public DateTime? Ngay { get; set; }
        public TimeSpan? Gio { get; set; }
        public string Noidung { get; set; }
        public int? Trangthai { get; set; }
        public string CreatedBy { get; set; }
        public string DtUpdate { get; set; }
        public TimeSpan? GioServer { get; set; }
        public DateTime? NgayServer { get; set; }
        public string HuyBoi { get; set; }
        public int? Chinhanh { get; set; }
    }
}
