using System;
using System.Collections.Generic;

namespace ApiCore_facebook.Models
{
    public partial class FbDonhangChitiet
    {
        public int Id { get; set; }
        public string Mahd { get; set; }
        public int? IdSp { get; set; }
        public string Tensp { get; set; }
        public int? Soluong { get; set; }
        public long? Giagoc { get; set; }
        public long? Thanhtien { get; set; }
    }
}
